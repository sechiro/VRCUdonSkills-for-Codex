# ENVIRONMENT_SETUP - [PROJECT_NAME]

## 1. 目的

- このプロジェクトを再現できる開発環境をそろえる
- Unity / SDK / UdonSharp / 検証手順の前提差を減らす

## 2. 基本環境

- Unity: `[UNITY_VERSION]`
- VRChat SDK: `[SDK_VERSION]`
- UdonSharp: `[UDONSHARP_SOURCE_OR_VERSION]`
- Creator Companion 使用有無: `[YES_OR_NO]`

## 3. セットアップ手順

1. `[INSTALL_VCC_OR_EQUIVALENT]`
2. `[CREATE_OR_OPEN_PROJECT]`
3. `[IMPORT_OR_RESOLVE_PACKAGES]`
4. `[IF_USING_UNITY_MCP_PLACE_PROJECT_ROOT_DOT_MCP_JSON]`
5. `[OPEN_MAIN_SCENE]`
6. `[RUN_REQUIRED_BUILDERS]`

## 4. 必須確認

- Scene Descriptor: `[CHECK_RULE]`
- UdonSharp Compile: `[CHECK_RULE]`
- Builder 実行後の階層: `[CHECK_RULE]`
- Networking 前提: `[CHECK_RULE]`
- Persistence 前提: `[CHECK_RULE]`

## 5. 検証手段

- Unity 上の確認: `[CLIENTSIM_OR_EDITOR_TEST]`
- VRChat 実機確認: `[IN_CLIENT_TEST]`
- 追加ツール: `[OPTIONAL_TOOLS]`

## 6. Unity MCP を使う場合

- `.mcp.json` は project root に置く
- 既定の接続先: `[UNITY_MCP_URL_OR_PROJECT_SPECIFIC_URL]`
- 用途は Editor 支援に限定し、Runtime 依存にはしない

## 7. 更新時のルール

- Unity / SDK 更新時はこの文書を更新する
- 公式参照の入口変更時は `docs/OFFICIAL_REFERENCE_MAP.md` も更新する
