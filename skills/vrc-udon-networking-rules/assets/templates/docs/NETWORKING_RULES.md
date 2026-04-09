# NETWORKING_RULES - [PROJECT_NAME]

## 基本原則

- 一時通知と持続状態を分離する
- 送信側だけでなく受信側でも権限検証する
- 送信者名の引数を信用しない
- Late Joiner が必要な状態は同期変数または復元可能な状態として設計する

## Network Event

- 用途: `[TEMPORARY_EVENT_USE]`
- 受信メソッドの条件: `[PUBLIC_OR_NETWORKCALLABLE_RULE]`
- 送受信権限制御: `[AUTH_RULES]`

## Sync Variables

- 用途: `[SYNC_USE]`
- 同期対象: `[SYNC_FIELDS]`
- 同期しない対象: `[LOCAL_ONLY_FIELDS]`

## Ownership

- 誰が状態を確定するか: `[OWNER_RULE]`
- Ownership 移譲条件: `[TRANSFER_RULE]`
- 暗黙挙動への依存: `避ける`

## Persistence がある場合

- Player Data 使用有無: `[YES_OR_NO]`
- Player Object 使用有無: `[YES_OR_NO]`
- Player Object 関連の Network ID 安定化ルール: `[KEEP_STABLE_OR_DESCRIBE_MIGRATION]`
- 初期化順ルール: `[RESTORE_RULE]`
- key 命名ルール: `[KEY_RULE]`

## 断定しない事項

- 公式根拠がないものは `Not documented / Uncertain (no official source available).` と明記する
