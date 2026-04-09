# Networking Guide

## この reference の目的

- Network Event と同期変数の使い分けを固定する
- Ownership の判断を文書化する
- Late Joiner、Player Data、Player Object、Persistence を同じ図で整理する

## 基本原則

- 一時通知は Network Event、持続状態は同期変数または復元可能データに分ける
- 状態正本は 1 つにする
- 送信者が主張する player id や owner 情報をそのまま信用しない
- 復元が必要な状態は Late Joiner 観点で先に考える

## Decision Flow

1. 今必要なのは瞬間通知か、状態共有かを決める
2. 状態共有なら、インスタンス内だけか、プレイヤーごと永続化も必要かを決める
3. Ownership が必要なら、誰が取得し、いつ手放すかを決める
4. Late Joiner が見落としてはいけない状態を列挙する
5. その結果を `docs/NETWORKING_RULES.md` に固定する

## Persistence との関係

- Persistence は過去セッションからの復元用であり、リアルタイム同期の代替ではない
- Player Data はプレイヤー単位の保存対象に向く
- Player Object はプレイヤーに紐づく実体が必要なときに使う
- Player Object が保存データや復元ロジックに関わるなら、関連する Network ID を変えない方針を明記する
- 初期化順が曖昧なまま導入しない

## 曖昧な事項の扱い

- 公式ドキュメントで根拠が弱い事項は `Not documented / Uncertain (no official source available).` と書く
- 推測と実測を混同しない
