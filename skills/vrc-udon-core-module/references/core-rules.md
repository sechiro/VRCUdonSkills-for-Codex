# Core Rules

## この reference の目的

- 再利用可能な UdonSharp モジュールの基本原則をそろえる
- 外部プロジェクトへ持ち出しても崩れにくい最小ルールを共有する
- プロジェクト固有要件をどこへ逃がすかを明確にする

## 基本方針

- UdonSharp は通常の C# より制約が強い前提で設計する
- 変化しやすい設定は Inspector から読める形に寄せる
- `List<T>`、`Dictionary`、`HashSet`、LINQ などは常用前提にしない
- Data Lists はデフォルト採用しない。明示指定された場合だけ `$vrc-udon-data-lists` を使う
- Runtime はできるだけ軽く保つ
- Scene 構築責務は `Editor/` に逃がせるなら逃がす
- プロジェクト固有仕様は `docs/` に残す

## 推奨フォルダ

- `AGENTS.md`
- `README.md`
- `docs/`
- `Editor/`
- `Scripts/` または `Runtime/`

## Builder First の原則

- シーン階層は手作業より Builder で再生成できる状態を優先する
- 生成物の規則は `docs/SCENE_OBJECT_SPEC.md` に残す
- UdonSharpBehaviour 自体は Builder の結果を受けて動く構成にする
- Inspector 設定も Builder 側で再接続できるならそうする
- 生成前提でない手修正は増やさない

## Runtime / Editor の分離

Runtime が担当するもの:

- 入力処理
- 状態遷移の実行
- Networking
- UI への反映

Editor が担当するもの:

- Scene 構築
- 命名
- 参照接続
- 初期配置の整形

## Documentation minimum

外部プロジェクトでは少なくとも次を持つことを推奨する。

- 読み順を示す `AGENTS.md`
- `docs/SYSTEM_SPEC.md`
- Builder で階層を生成する場合は `docs/SCENE_OBJECT_SPEC.md`
