# Edit Failure Analysis

## この Skill が前提にする主な失敗原因

### 1. `apply_patch` の失敗

今回の運用では、`apply_patch` が次のような形で失敗することがあった。

- `windows sandbox: setup refresh failed with status exit code: 1`

これは編集内容そのものではなく、ツール実行環境側の問題である可能性が高い。したがって、

- パッチ内容が悪いと即断しない
- 再試行しても同種エラーなら別編集手段へ切り替える

が基本になる。

### 2. PowerShell の here-string / バッククォート展開

PowerShell では、ダブルクォート here-string や文字列置換で次が壊れやすい。

- Markdown のバッククォート
- URL
- 行継続や埋め込み変数
- 置換時の制御文字混入

特に `README.md` や日本語文書では、見た目は近くても不可視制御文字や `?` が混入しやすい。

### 3. エンコーディングと BOM

Windows では、同じ UTF-8 でも BOM の有無で扱いが変わることがある。

- `.cs` や `.yaml` は BOM なしが安全なことが多い
- `.md` は UTF-8 で読めればよいが、意図しない BOM や文字化けが混入しうる

### 4. 日本語を含むコードコメントや見出し

日本語自体が悪いわけではないが、Windows / PowerShell / 文字列埋め込みの組み合わせでは事故率が上がる。

- コードファイルは ASCII 優先
- Markdown は日本語可。ただし編集後検証を必須にする

## 切り分けの考え方

- `apply_patch` エラー: ツール環境問題寄り
- `?` 混入: 文字列展開か文字コード問題
- `\ufeff` や先頭不可視文字: BOM 混入
- 一部だけ壊れる: 文字列置換や here-string 展開事故
