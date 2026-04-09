# VS Code Codex File Reference Rules

## Purpose

Use this reference when a session needs a copy-pasteable rules block for file references in the VS Code Codex chat UI.

## Reusable Rules Block

```text
このセッションでは、ファイル参照を必ず次のルールで出力してください。

【現在のワークスペース ルート】
- まず、今開いているプロジェクトの実際の絶対パスを確認する
- `[ワークスペースルート絶対パス]` は説明用の仮置きであり、実際に出力するときは必ず解決済みの実パスへ置き換える
- `[ワークスペースルート絶対パス]` が未置換のままなら、そのまま使わず実値を確認してから出力する

【最優先ルール】
- ファイル参照は、必ずインラインコードで単独記載する
- ファイル参照は `` `@` + 絶対パス `` 形式にする
- プロジェクト内ファイルのパスは必ず `` `@[解決済みワークスペースルート絶対パス]/...` `` 形式にする
- Windows環境でも `/` 区切りを使う
- 行番号を指定する場合は `#Lline` または `:line[:column]` を付与する
- ファイル参照はコードブロック内に入れない
- `@` 付き参照は裸テキストで書かず、必ずインラインコードで囲む
- プロジェクト内ファイル参照は、解決済みワークスペースルートを使った実パスで出力するまで確定させない
- 参照先パスが不明な場合は推測せず、先に確認してから出力する

【禁止事項】
- Webviewリンク（`https://file+.vscode-resource...`）を使わない
- `file://` や `vscode://` を使わない
- Markdownリンクをファイル参照の代替手段として使わない
- 相対パスを使わない
- ファイル名だけの記載を使わない
- 略記したパスを使わない
- プレースホルダのままのパスを使わない

【出力形式】
- ファイルのみ:
  `@[解決済みワークスペースルート絶対パス]/README.md`
- 行番号付き:
  `@[解決済みワークスペースルート絶対パス]/docs/TASK_PROMPT_TEMPLATE.md#L32`
- 別記法の例:
  `@[解決済みワークスペースルート絶対パス]/src/app.ts:42`
- ワークスペース外ファイル:
  `@C:/Users/<your-user>/tools/example-tool/config.json#L12`

【回答前セルフチェック】
- `https://file+.vscode-resource...` が含まれていない
- `[ワークスペースルート絶対パス]` が残っていない
- `[解決済みワークスペースルート絶対パス]` が説明用のまま残っていない
- すべてのファイル参照が `` `@` + 絶対パス `` になっている
- プロジェクト内ファイル参照が解決済みの実パスになっている
- 行番号付き参照は `#Lline` または `:line[:column]` になっている
- ファイル参照がコードブロックではなくインラインコードで出力されている
```

## Invocation Examples

- `Use $vscode-file-links at [vscode-file-links の絶対パス] and format all file references for this session.`
- `Use $vscode-file-links at [vscode-file-links の絶対パス] and rewrite this prompt template for a different workspace root.`
