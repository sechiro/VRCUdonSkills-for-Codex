# VRCUdonSkills for Codex

VRChat UdonSharp 開発向けに整理した、Codex 用オリジナル Skill 集です。

この公開版は、`VRCUdonSkills` で育てた知見のうち、外部配布に向く self-contained な Skill とテンプレートだけを切り出しています。内部の案件整理メモや参照元プロジェクト一覧、ローカル環境前提の知識ベース本体は含めていません。

## 収録 Skill

- `vrc-udon-project-docs`: project-owned な文書土台、環境再現文書、公式参照マップ、補助テンプレート
- `vrc-udon-core-module`: Builder First、Runtime / Editor 分離、再利用モジュールの基本骨格
- `vrc-udon-ui-menu`: ワールド空間 UI、画面遷移、Pickup と UI の干渉整理
- `vrc-udon-networking-rules`: Sync、Ownership、Network Event、Late Joiner のルール固定
- `vrc-udon-persistence-sync`: local / synced / Player Data / Player Object / Persistence の選定
- `vrc-udon-heavy-runtime`: 高負荷 runtime、段階実行、replay / rebuild の整理
- `vrc-udon-unity-mcp`: Unity MCP を Editor 支援として扱うための運用ルール
- `vrc-udon-fukuro-udon`: Fukuro Udon 導入時の既定差分
- `vrc-udon-data-lists`: DataList / Data Containers を明示 opt-in で扱う補助
- `vrc-udon-knowhow-transfer`: import / export ベースの know-how 受け渡し整理
- `codex-edit-stability-windows`: Windows 上の Codex 編集事故を減らす運用ルール
- `vscode-file-links`: VS Code Codex ローカルチャット用のファイル参照形式統一

## 公開方針

- 公開対象は、他プロジェクトへ持ち出せる Skill 本体、references、assets、agents metadata に限定する
- 案件固有の分析ドキュメント、参照元プロジェクトのローカル絶対パス一覧、内部知識ベースの運用資料は含めない
- 各 Skill は単体でも読めるようにし、公開リポジトリ外の `docs/` を前提にしない
- project-owned な仕様は Skill に固定せず、導入先プロジェクトの `AGENTS.md` や `docs/` へ移す前提を維持する

## 使い方

1. `skills/` 配下から使いたい Skill フォルダを選ぶ
2. その Skill フォルダを Codex の Skill ディレクトリへ配置する
3. 必要に応じて `SKILL.md`、`references/`、`assets/` を読みながら導入先へ反映する

Skill をまとめて扱いたい場合は、このリポジトリごと参照して必要なフォルダだけ取り込んでください。

## ライセンス

このリポジトリ全体は MIT License です。

一部テンプレートには別作者の MIT ライセンス文面を同梱しています。該当する再配布条件は各 Skill 内の `LICENSES/` やライセンス注記をそのまま維持してください。
