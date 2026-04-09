# UdonSharpProgramAsset Auto Generation - [PROJECT_NAME]

## 1. 目的

- `UdonSharpBehaviour` 追加時の `U# Program Asset` 手動生成漏れを防ぐ
- 対象フォルダだけで自動生成を有効にする

## 2. 配置先

- スクリプト: `[EDITOR_SCRIPT_PATH]`
- 対象フォルダ: `[TARGET_ROOT_FOLDERS]`

## 3. 運用ルール

- 対象は UdonSharp を含むフォルダだけに限定する
- `.asset` は `.cs` と同じ階層に生成する
- 既存 `.asset` は上書きしない
- 自動生成対象外のフォルダでは動かさない

## 4. 必須確認

- `TargetRootFolders` が実パスへ置換されている
- UdonSharpBehaviour の `.cs` 追加時に `.asset` が生成される
- UdonSharp コンパイルがエラーなく通る

## 5. 参照元

- `[REFERENCE_URL]`

## 6. 出典とライセンス

- 参照元 URL: `https://gist.github.com/nemurigi/dea7c0a1fb94f7b9cf1c36481a459ded`
- 配布元ページ上のライセンス表記: `MIT License`
- このリポジトリでは、対象フォルダ限定の条件を追加したテンプレートとして同梱している
- 導入先へ再配布する場合も、参照元 URL とライセンス情報が追える状態を保つ
- 同梱用ライセンス文面は LICENSES/UdonSharpProgramAssetAutoGenerator.MIT.txt を使う
