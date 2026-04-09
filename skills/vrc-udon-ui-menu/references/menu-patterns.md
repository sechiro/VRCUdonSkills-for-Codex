# Menu Patterns

## この reference の目的

- ワールド空間 Canvas の基本構成をそろえる
- ページ切り替え系 UI の Controller 設計をそろえる
- 手持ち情報端末 UI と interactable object 上の UI の違いを明確にする
- Pickup と UI の衝突を避ける

## ページ切り替えメニュー

### Controller の置き方

- 画面群を管理する Controller を sibling または parent に置く
- screen root ごとに表示切り替えできるようにする

### 公開イベント

- 基本は `UdonBehaviour.SendCustomEvent(string)` で呼べる名前にそろえる
- `GoHome`、`GoBack`、`Close`、`NextPage`、`PrevPage` を優先採用する

### 開閉導線

- 最低限 `Open` と `Close` を持つ
- 誤操作を避けたい場合は hold timer や confirm UI を検討する
- 閉じたあとに操作不能になる状態を作らない

### Pickup 併用

- 手持ち UI は Pickup Collider を主軸にする
- 同じ領域に UI Collider を置く場合は Pickup との干渉を確認する
- 落下中や所持解除時の表示挙動を決める

### Desktop / VR 差分

- Desktop はクリック中心
- VR は use-hold やトリガー押下を前提にする
- 入力差を Runtime API の分岐に閉じ込める

## 失敗しやすい点

- 画面ごとに状態を持ちすぎる
- Controller が無く遷移規則が散る
- 閉じる戻るの定義が曖昧
- 手持ち UI の姿勢が安定しない
- Pickup Collider と UI Collider の責務が曖昧
