# VRAM / ダウンロードデータ量最適化ガイド

## 目的

VRChat ワールド内で画像やスライドを表示するギミックを作るときに、次の2つを切り分けて適切な選択ができるようにする。

- 配布データ量
- 実行時 VRAM 使用量

このページは Skill ではなく、複数 Skill から参照できる横断ノウハウとして扱う。

## 対象

- 3D サーフェスにテクスチャを貼るオブジェクト（通常の3Dオブジェクト全般）
- World Space UI で画像を表示するオブジェクト
- `VRCImageDownloader` を使う動的画像ギミックなど

## 非対象

- 個別アセット名や個別ファイル設定
- そのまま流用する完成実装

## まず固定する前提

### 1. 配布データ量と VRAM は別物

- Crunch は主に配布データ量を下げる
- GPU 圧縮フォーマットは配布データ量と VRAM の両方に効く
- `VRCImageDownloader` はバンドルを軽くできるが、実行時画像は非圧縮系フォーマットで展開される

Unity 公式で確認できること:

- Unity Manual の Texture compression formats は、圧縮フォーマットが build size、loading time、runtime memory usage に影響すると説明している
- 同ページは Crunch について、CPU で DXT / ETC に展開して GPU へ送るため、runtime memory usage には影響しないと説明している

### 2. UI と 3D サーフェスでは最適解が違う

- UI コンポーネントでも、World Space の表示オブジェクトとして使う場合は 3D サーフェスに近い判断になる
- 近距離から見る前提の UI は、表示用に作った解像度をそのまま見せる前提になりやすい
- 3D サーフェスは距離で見え方が変わるため、ミップマップの恩恵が大きい

### 3. まず構造を固定してから圧縮を詰める

- 先に表示構造を決める
- その後で Texture Importer の設定を詰める
- さらに必要なら atlas 形状、解像度、動的配信を検討する

## 基本判断

### 画像の持ち方を決めるときの優先順

1. まず、解像度や表現方式を見直して、本当にその表示品質が必要か確認する
2. 固定コンテンツでよいなら、バンドル内テクスチャを優先する
3. バンドル内テクスチャを使う場合は、プラットフォーム別圧縮、`Read/Write Enabled`、ミップマップ設定を見直す
4. 実行時更新が必要な場合だけ `VRCImageDownloader` を検討し、解像度上限、レート制限、`Dispose()` を前提に設計する

## Texture Importer の基本方針

### 共通

- `Read/Write Enabled` は、特別な理由がなければデフォルトの `OFF` のままでよい
- 不要な alpha は持たせない
- `Max Texture Size` は必要十分にとどめる
- 問題がなければ `Automatic` を基本にし、個別に最適化したい場合だけ PC / Android の override を使う

Unity 公式で確認できること:

- Texture Import Settings は `Read/Write` を有効にすると、スクリプトアクセス用のコピーを内部に持つため Texture に必要なメモリが倍増すると説明している
- Texture Import Settings は `Max Size` を超えると、import 時にその上限に収まるよう縮小される前提で扱う
- Platform-specific overrides を使うと、Windows 系では DXT1 / DXT5 / BC7、Android 系では ETC / ETC2 / ASTC 系など画像の圧縮方法を個別に選べる
- Unity の `Automatic` はプラットフォームに応じて適切な形式を自動選択するため、まずはそれで問題ないかを見る運用ができる

### 推奨の出発点

まずは `Automatic` で確認し、品質や容量を個別に詰めたい場合だけ override を明示する。

| 用途 | PC | Android / Quest | 備考 |
|---|---|---|---|
| 透過なし画像 | DXT1 | ASTC 6x6 | まずここから始める |
| 透過あり画像 | BC7 か DXT5 | ASTC 6x6 | 画質優先なら PC は BC7 を検討 |
| 容量優先の Quest | - | ASTC 8x8 | テキスト主体なら実機確認が必要 |

補足:

- ASTC 8x8 は容量をさらに下げやすいが、細い文字や線は崩れやすい
- BC7 は DXT5 より高品質になりやすいが、VRAM は軽くならない

### 主要フォーマットの bpp と VRAM 目安

| フォーマット | bits/pixel | VRAM（4K: 4096x4096） | VRAM（1920x1080） | 備考 |
|---|---|---|---|---|
| RGBA32（非圧縮） | 32 bpp | ~64.0 MB | ~7.9 MB | 実行時ダウンロード画像の代表例 |
| RGB24（非圧縮） | 24 bpp | ~48.0 MB | ~5.9 MB | |
| DXT1（PC、透過なし） | 4 bpp | ~8.0 MB | ~1.0 MB | PC 標準の出発点 |
| DXT5 / BC3（PC、透過あり） | 8 bpp | ~16.0 MB | ~2.0 MB | |
| BC7（PC、高品質） | 8 bpp | ~16.0 MB | ~2.0 MB | DXT5 より高品質 |
| ASTC 4x4（Quest） | 8 bpp | ~16.0 MB | ~2.0 MB | 高品質寄り |
| ASTC 6x6（Quest） | 3.56 bpp | ~7.1 MB | ~0.9 MB | 品質とサイズのバランス点 |
| ASTC 8x8（Quest） | 2 bpp | ~4.0 MB | ~0.5 MB | テキストは要確認 |
| ETC2 RGB（Quest 旧来） | 4 bpp | ~8.0 MB | ~1.0 MB | ASTC 非対応寄りの基準 |

補足:

- ここでの値は、`bits/pixel` から計算した概算値
- ミップマップを有効にすると、これらの値に加えて概ね `+33%` を見る

## ミップマップの扱い

### 3D サーフェス

推奨:

- 板ポリ、壁、床、ポスターなど距離で見え方が変わる表示は `Generate Mipmaps: ON` を基本にする
- ワールド内のオブジェクトで画像を表示する用途は、基本的に 3D サーフェスと同様に扱う

理由:

- 遠距離でのシマリング抑制
- キャッシュ効率の改善
- VRChat の mipmap streaming の恩恵を受けやすく、ゲーム内での VRAM 使用量削減につながりやすい

Unity 公式で確認できること:

- Unity Manual は、mipmap を使わないと大きな performance loss がありうること、aliasing や shimmering の回避に重要であることを説明している
- Unity Manual の Mip Map Streaming は、必要な mip だけを読み込んで GPU memory を節約する仕組みとして説明している

### UI

推奨:

- UI コンポーネントを World Space の表示オブジェクトとして使う場合は、3D サーフェスと同様に `Generate Mipmaps: ON` を基本にする
- ただし、近距離から見る前提の UI では、`Generate Mipmaps: OFF` を検討する

理由:

- World Space の表示オブジェクトとして使う UI は、見え方の判断が 3D サーフェスに近い
- 近距離で使う UI は、表示サイズを見越して解像度を作ることが多い
- その場合、低 Mip を積極的に使う場面が少ない
- ミップマップぶんのメモリ増加だけ受けやすい

Unity 公式で確認できること:

- Unity Manual は mipmap を「遠いオブジェクトで小さい Texture を使う」仕組みとして説明している
- Unity Manual は GUI textures を、常用 mipmap の例外側として挙げている

注意:

- VRChat では、World Space の表示オブジェクトに UI コンポーネントを使い、遠距離から UI を見る構成がありうる
- その場合は 3D サーフェスに近い見え方になるため、mipmap を使う余地がある
- ワールド内オブジェクトとして画像を見せる用途では、まず 3D サーフェス側の判断を優先する
- 近距離 UI では OFF を検討し、World Space UI は距離や見せ方に応じて実機確認する

### 目安

- ミップマップを有効にすると、総メモリは概ね元の約 1.33 倍になる
- ただし VRChat では mipmap streaming が有効なため、ゲーム内で常にその全量を高解像度のまま保持し続けるとは限らない
- 距離で見え方が変わる texture では、mipmap を持たせた方が streaming の恩恵を受けやすく、結果としてゲーム内の VRAM 使用量削減につながりやすい

Unity 公式で確認できること:

- Unity Manual は、mipmap の使用でメモリが 33% 増える目安を示している

例:

- 4K テクスチャ 1 枚を 8 bpp 相当で保持する場合、ベースよりミップ込みの方が約 33% 増える前提で見る

### VRChat の DPID ミップマップサポート

- VRChat SDK `3.7.1` では、`2024-09-20` に DPID mipmaps が追加された
- SDK `3.7.1` 時点では、これは explicit opt-in が必要な beta 機能として案内されている
- `Kaiser` filtering を選んだ texture に対して DPID を適用する整理になっている
- VRChat は、world / avatar textures の遠距離での鮮明さ改善を主目的として説明している
- `Alpha Is Transparency` を有効にした texture での扱いも改善が進められている

VRChat 公式で確認できること:

- SDK `3.7.1` は、DPID が world / avatar textures の sharpness を大きく改善すると説明している
- クライアント `2024.3.2` は、portal backgrounds、nameplates、user icons、Udon-loaded images の legibility / sharpness 改善を説明している
- クライアント `2024.4.2` は、DPID を再有効化し、`AlphaIsTransparency` texture でより鮮明になったと説明している
- `2026-04-17` 時点で確認した公開 SDK リリースノートの範囲では、DPID が beta 扱いを外れたという明示は見つからなかった

現時点の扱い:

- 少なくとも公式リリースノート上では、`3.7.1` の beta / opt-in 表記が確認できる
- その後の改善リリースはあるが、beta 卒業を明示する一次情報は確認できていない
- そのため、このガイドでは「beta として導入された機能で、現時点でも正式化の明示は未確認」と扱う

採用判断:

- mipmap を使う 3D サーフェスでは、`Kaiser` を使う理由があるかを先に確認する
- DPID は、mipmap を使う texture の遠距離での見え方を改善する機能として扱う
- 画質改善の恩恵は期待できるが、VRAM を直接削減する機能としては扱わない

### VRChat の mipmap streaming

- VRChat クライアント `2024.4.2` は、`2024-12-05` に mipmap streaming を有効化した
- worlds と avatars に対して、必要に応じて低解像度 mip から VRAM へ動的に読み込む仕組みとして説明されている
- Android / Quest では利用可能な shared memory に応じて budget を動的更新し、PC では起動時に GPU VRAM を見て budget を決める
- avatars では `Mipmap Streaming Priority` が常に `0` として扱われ、worlds では texture priority がそのまま残る
- `2024-12-12` の `2024.4.2p2` では、mobile devices と standalone VR 向けに threshold 調整が入り、blur を減らす方向の修正が入っている

VRChat 公式で確認できること:

- VRChat は、この機能により lower-resolution textures を必要に応じて使い、stuttering、avatars failing to load、hard crashes にぶつかる前に、より多くを VRAM に載せられると説明している
- Quest / Android では、crash や「ホームに飛ばされる」事象の削減を狙った budget 更新であると説明している

実務上の見方:

- VRChat では mipmap streaming が有効になっているため、mipmap を効果的に使える texture の方が、ゲーム内では常に最大解像度を保持し続ける前提より VRAM を削減しやすい
- 特に距離で見え方が変わる world 内オブジェクトでは、mipmap を持たせた方が VRAM と見た目の両面で有利になりやすい
- 逆に、mipmap を使わない texture は streaming の恩恵を受けにくい

### mipmap streaming の VRAM 削減効果

- VRChat 公式資料では、mipmap streaming の VRAM 削減量について具体的な百分率は見つからなかった
- 一方で Unity 公式の Mipmap Streaming system は、必要な mip level だけを読み込むことで GPU memory を節約すると説明している
- Unity は、Viking Village demo project で texture memory を `25 - 30%` 節約した実例を示している

このガイドでの扱い:

- `25 - 30%` は Unity のデモプロジェクトでの実例であり、VRChat world で常に同程度削減できるとは断定しない
- VRChat 側では、scene 構成、camera 距離、texture の使われ方、platform ごとの budget 制御に依存するとみなす
- そのため、実案件では「VRChat の mipmap streaming を活かすため、mipmap を効果的に使える texture は VRAM 削減の観点でも有利」と扱う
- ただし削減率の定量値は条件依存であり、個別案件では実機確認を前提にする

## Crunch の位置づけ

- 主な効果は配布データ量の削減
- VRAM 削減目的では使わない
- テキストや高コントラスト画像では劣化確認が必要
- Unity / SDK 更新時の互換性や見え方の差は再確認する

Unity 公式で確認できること:

- Unity Manual は Crunch を disk space 重視の手段として位置づけ、runtime memory usage には効かないと明記している

VRChat 公式で確認できること:

- VRChat の Android Content Optimization は、Crunch compression は in-memory size には効かず、download size にだけ効くと明記している
- 同ページは、Android 向けコンテンツサイズは Crunch なしでも上限内に収めるべきとしている
- また、Unity の新しいバージョンで互換性のない Crunch が使われると、後で不具合が出る場合がある点にも注意を促している

採用判断:

- 初回ダウンロード待ち時間を削りたいときは有効
- 実行時 VRAM が問題なら別の手段を優先する

## `VRCImageDownloader` の位置づけ

### 向いている場面

- イベントごとに画像が差し替わる
- ワールド再アップロードなしで内容更新したい
- URL ベースの動的配信が本質要件になっている

### 向いていない場面

- 固定スライドデッキ
- 同じ画像をいつも表示するだけの UI
- VRAM 効率を最優先したい場面

### 公式に確認しやすい制約

- 最大解像度は `2048 x 2048`
- ダウンロードは `5秒に1回` が基準
- `TextureInfo.GenerateMipmaps` の既定値は `false`
- 利用後の `Dispose()` が必要

### VRAM 効率の注意

- `VRCImageDownloader` で読み込んだ画像は、VRChat 公式の説明では `RGBA32`、`RGB24`、`R8` などの非圧縮系フォーマットで扱われる
- そのため、GPU 圧縮 texture を使う場合と比べると VRAM 効率は良くない
- 固定画像を表示する用途では、圧縮済み texture をバンドルに含める方が VRAM 面では有利になりやすい

| フォーマット | VRAM（2048x2048） | VRAM（1920x1080） | 備考 |
|---|---|---|---|
| RGBA32 | ~16.0 MB | ~7.9 MB | alpha あり画像の代表例 |
| RGB24 | ~12.0 MB | ~5.9 MB | alpha なし画像の代表例 |
| R8 | ~4.0 MB | ~2.0 MB | grayscale 系 |
| DXT1 | ~2.0 MB | ~1.0 MB | 比較用: 圧縮済み texture |
| DXT5 / BC3 / BC7 | ~4.0 MB | ~2.0 MB | 比較用: 圧縮済み texture |
| ASTC 6x6 | ~1.8 MB | ~0.9 MB | 比較用: Quest 向け圧縮 texture |

見方:

- `2048 x 2048` は `VRCImageDownloader` の最大解像度
- たとえば alpha あり画像を `RGBA32` で持つと、`2048 x 2048` で約 `16 MB` になる
- 同程度の見た目を圧縮 texture で持てる場合、PC では `DXT5 / BC3 / BC7`、Quest では `ASTC 6x6` の方が VRAM をかなり抑えやすい
- そのため、`VRCImageDownloader` は「配布バンドルを軽くできる」一方で、「ゲーム内 VRAM 効率は良くない」というトレードオフで扱う

補足:

- alpha なし画像を `VRCImageDownloader` で 1 枚ずつ読み込み、前の画像をそのつど適切に `Dispose()` する運用なら、同解像度の画像をすべてバンドル内 `DXT1` で常時保持する場合より、ゲーム内 VRAM 使用量が小さくなる場合がある
- たとえば `1920 x 1080` では、`RGB24` の 1 枚は約 `5.9 MB`、`DXT1` の 1 枚は約 `1.0 MB` なので、7 枚をすべて `DXT1` で保持すると約 `7 MB` になる
- このため、「常に 1 枚だけ保持する `RGB24`」と「7 枚以上を同時保持する `DXT1`」の比較では、前者の方が小さくなることがある
- ただしこれは、同時保持しないこと、前画像を確実に `Dispose()` すること、表示切り替え時の一時的な重なりを小さくできることが前提
- また、`Dispose()` した画像を再表示したい場合は通常は再ダウンロードが必要になるため、切り替えのたびにネットワーク負荷が上がりやすい
- また、バンドル内 texture 側は VRChat の mipmap streaming により実ゲーム中の使用量が下がる場合があるため、常に単純比較どおりになるとは限らない

### 扱いの注意

- ダウンロード画像は GPU 圧縮前提で設計しない
- URL 制限やホワイトリスト前提を project-owned 文書へ残す
- Quest での採用は、「禁止の明記がない」ことと「推奨される」ことを分けて扱う

## ざっくりした選定表

| 目的 | 第一候補 | 補足 |
|---|---|---|
| 遠くから見る 3D ポスターをきれいにしたい | GPU 圧縮 + mipmap ON | DPID や streaming の恩恵を受けやすい |
| 近距離から見る前提の UI を軽くしたい | GPU 圧縮 + mipmap OFF 検討 | 表示サイズを見越した UI では OFF が有力 |
| ダウンロードサイズを減らしたい | Crunch 検討 | VRAM は減らない |
| 頻繁に更新される画像を出したい | `VRCImageDownloader` | VRAM と寿命管理は重くなりやすい |
| Quest 容量をさらに削りたい | ASTC 8x8 検討 | 文字主体なら要実機確認 |

## 公開ガイドとしての扱い

- このガイドは、画像表示ギミックの VRAM / ダウンロードデータ量最適化を外部公開向けに一般化したもの
- 個別案件では、採用フォーマット、解像度、例外条件を project-owned 文書へ明記する
- atlas による Material 削減や描画負荷の話は、この公開版には含めていない

## 公式参照先

- VRChat Creation: Image Loading
  - `https://creators.vrchat.com/worlds/udon/image-loading/`
- VRChat Creation: Release 3.7.1
  - `https://creators.vrchat.com/releases/release-3-7-1/`
- VRChat Docs: VRChat 2024.3.2
  - `https://docs.vrchat.com/docs/vrchat-202432`
- VRChat Docs: VRChat 2024.4.2
  - `https://docs.vrchat.com/docs/vrchat-202442`
- VRChat Docs: VRChat 2024.4.2p2
  - `https://docs.vrchat.com/docs/vrchat-202442p2`
- VRChat Creation: Performance Ranks
  - `https://creators.vrchat.com/avatars/avatar-performance-ranking-system`
- VRChat Creation: Release 3.7.5
  - `https://creators.vrchat.com/releases/release-3-7-5/`
- VRChat Creation: Android Content Optimization
  - `https://creators.vrchat.com/platforms/android/quest-content-optimization`
- Unity Manual: Texture compression formats
  - `https://docs.unity3d.com/2022.3/Documentation/Manual/class-TextureImporterOverride.html`
- Unity Manual: Texture Import Settings
  - `https://docs.unity3d.com/2022.3/Documentation/Manual/class-TextureImporter.html`
- Unity Manual: Mip Map Streaming
  - `https://docs.unity3d.com/2022.3/Documentation/Manual/TextureStreaming.html`
- Unity Manual: Mipmap Streaming system
  - `https://docs.unity3d.com/ja/2022.2/Manual/TextureStreaming.html`
- Unity Scripting API: `EditorUserBuildSettings.overrideMaxTextureSize`
  - `https://docs.unity3d.com/cn/2022.3/ScriptReference/EditorUserBuildSettings-overrideMaxTextureSize.html`
