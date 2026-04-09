# AGENTS.md - [PROJECT_NAME]

このドキュメントは `Assets/[PATH_TO_MODULE]` の開発ルールです。
このプロジェクトで作業する AI エージェントは、分析、提案、修正の前にここで指定した順番で文書を読むこと。

## REQUIRED READING ORDER

1. `AGENTS.md`
2. `docs/SYSTEM_SPEC.md`
3. `docs/ENVIRONMENT_SETUP.md`
4. `docs/OFFICIAL_REFERENCE_MAP.md`
5. `docs/UdonSharpProgramAsset_AutoGeneration.md` がある場合は読む
6. `docs/SCENE_OBJECT_SPEC.md` がある場合は読む
7. プロジェクト固有の追加仕様書

## Fixed Environment

- Unity: `[UNITY_VERSION]`
- VRChat SDK: `[SDK_VERSION]`
- UdonSharp: `[UDONSHARP_SOURCE]`

## Core Rules

- UdonSharp は制約前提で設計する
- 固定長配列と手動ループを優先する
- Builder First を基本にする
- Runtime と Editor の責務を分離する
- 公式確認がない仕様は断定しない

## Editing Boundaries

主な作業対象:

- `Assets/[PATH_TO_MODULE]/`

通常は編集しない:

- `Library/`
- `Temp/`
- `Logs/`
- `ProjectSettings/`
- `Packages/`

## Language Policy

- 回答言語: 日本語
- コードコメント: 日本語
- 識別子: 英語
