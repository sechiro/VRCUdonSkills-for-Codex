# Sync Decision Guide

## 目的

状態ごとに、local、sync、Player Data、Player Object、Persistence のどれを使うかを素早く決める。

## 判断順

1. 他プレイヤーに即時共有が必要か
2. Late Joiner に復元させる必要があるか
3. セッションをまたいで保存する必要があるか
4. プレイヤー単位か、ワールド共有か
5. 実体 Object が必要か

## 選び方

### Local state

- 自分だけが見えればよい
- 再入室や再参加で消えてよい
- 共有整合性が不要

### Synced state

- インスタンス内で共有する必要がある
- Late Joiner にも見えてほしい
- Ownership と更新責任者を決められる

### Player Data

- プレイヤーごとに保存したい
- セッションをまたいで残したい
- key と復元順を設計できる

### Player Object

- プレイヤーごとの実体や参照先が必要
- ローカルデータだけでは足りない
- 生成タイミングと寿命を管理できる
- Persistence や保存データと結びつく場合は、関連する Network ID を不用意に変えない
- PlayerObject を再生成、差し替え、Prefab 入れ替え、シーン再配線するときは、保存データへの影響を先に確認する

## よくある失敗

- Local state を同期前提で書いてしまう
- 同じ値を sync と persistence の両方で曖昧に持つ
- Ownership 未定のまま synced state を増やす
- 復元前提なのに初期化順を決めない
- PlayerObject 関連の Network ID 変更で保存データ互換を壊す
