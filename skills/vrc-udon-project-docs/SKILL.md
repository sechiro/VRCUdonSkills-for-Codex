---
name: vrc-udon-project-docs
description: 外部の VRChat UdonSharp プロジェクトに project-owned な AGENTS.md、README、SYSTEM_SPEC、SCENE_OBJECT_SPEC、ENVIRONMENT_SETUP、OFFICIAL_REFERENCE_MAP を配布可能な形でそろえるときに使う。実装前の文書土台、環境再現用の文書セット、U# Program Asset 自動生成用の Editor テンプレート、必要なら Unity MCP 用 `.mcp.json` テンプレートを作りたい場合に使う。モジュール構造そのものは `vrc-udon-core-module` に分ける。
---

# VRC Udon Project Docs

## 役割

外部の VRChat UdonSharp プロジェクトに、そのプロジェクト自身が持つべき基本文書一式を入れる。Skill 側の知識と、導入先プロジェクト固有の仕様を分離するための土台を作る。

## この Skill を最初に使う場面

- 新規プロジェクトや外部配布の土台を作る
- project-owned 文書を先にそろえたい
- 環境再現手順や公式参照先も含めて渡したい

## この Skill を使わない場面

- モジュールの構造設計だけを進めたい場合: `vrc-udon-core-module`
- networking 文書だけを配りたい場合: `vrc-udon-networking-rules`

## 進め方

1. `references/doc-pack-guide.md` を読む。
2. `assets/templates/` から必要なテンプレートを配置する。
3. 導入先プロジェクト固有の内容で埋める。
4. 最低限そろえる。
   - `AGENTS.md`
   - 必要なら `.mcp.json`
   - `docs/SYSTEM_SPEC.md`
   - `docs/ENVIRONMENT_SETUP.md`
   - `docs/OFFICIAL_REFERENCE_MAP.md`
   - 必要なら `docs/UdonSharpProgramAsset_AutoGeneration.md`
   - Builder があるなら `docs/SCENE_OBJECT_SPEC.md`
   - 入口になる `README.md`
5. 仕様追加時の更新責任を project-owned 文書側に寄せる。
6. imported 情報はそのまま正本にせず、project-owned 文書へ整理して反映する。
7. UdonSharp の `.asset` 手動生成漏れを減らしたい案件では `assets/templates/Editor/UdonSharpProgramAssetAutoGenerator.cs` も配る。
8. Unity MCP を使う案件では `assets/templates/.mcp.json` も配り、接続先を project root で管理する。

## 担当範囲

- project-owned 文書雛形の配布
- 環境再現文書の配布
- 公式参照マップの配布
- 外部由来コードのライセンス同梱
- 必要な Editor 補助設定テンプレートの配布

## 注意点

- Skill 側の説明をそのまま仕様書にしない
- 実装責務と Ownership を文書で言い切る
- imported 参照は project-owned な文章へ翻訳してから残す
- 環境再現に必要な公式参照先も project-owned 文書として残す
- `.mcp.json` は Unity MCP を使う案件だけに配る
