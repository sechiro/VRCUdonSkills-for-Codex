# [PROJECT_NAME]

このリポジトリは、[PROJECT_NAME] の VRChat ワールド開発を進めるための project-owned な作業領域です。

この README は、このプロジェクトで何を作っているか、どの文書を先に読むべきか、どのフォルダに何を置くかを共有するための入口です。

## このプロジェクトの目的

- [PROJECT_GOAL]
- [PROJECT_GOAL_2]
- [PROJECT_GOAL_3]

## このリポジトリで管理するもの

- ワールド機能の仕様と実装方針
- UdonSharp / Builder / Editor の責務分離
- セットアップ手順と検証手順
- 公式ドキュメントの参照先
- 必要に応じた外部参照の採用記録

## フォルダ構成

- `docs/`: このプロジェクトの正本ドキュメント
- `Editor/`: Builder やセットアップ支援、Editor 自動化
- `Scripts/` または `Runtime/`: UdonSharp ランタイム
- `LICENSES/`: 外部由来コードや同梱物のライセンス情報
- `import/`: 参照専用の外部資料がある場合のみ
- `export/`: 他案件向けに共有する場合のみ

## 最初に読むファイル

1. `AGENTS.md`
2. `docs/SYSTEM_SPEC.md`
3. `docs/ENVIRONMENT_SETUP.md`
4. `docs/OFFICIAL_REFERENCE_MAP.md`
5. `docs/UdonSharpProgramAsset_AutoGeneration.md` がある場合
6. `docs/SCENE_OBJECT_SPEC.md` がある場合
7. その他の project-owned 仕様書

## 現在の前提

- Unity: `[UNITY_VERSION]`
- VRChat SDK: `[SDK_VERSION]`
- UdonSharp: `[UDONSHARP_SOURCE_OR_VERSION]`
- 主な対象: `[MAIN_FEATURE_AREA]`

## 補足

- 外部記事や外部コードは補助資料として参照し、採用した内容だけを project-owned 文書へ反映する
- 外部由来コードを同梱する場合は、`LICENSES/` にライセンスと出典情報を残す
