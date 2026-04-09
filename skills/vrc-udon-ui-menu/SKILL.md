---
name: vrc-udon-ui-menu
description: VRChat UdonSharp でワールド空間 UI、ページ切り替えメニュー、手持ち UI、Pickup と連動する操作導線を設計・実装するときに使う。手持ち情報端末 UI、メニュー遷移、閉じる戻る導線、Desktop / VR 両対応の入力整理が必要な場合に使う。ネットワーク仕様そのものは `vrc-udon-networking-rules` に分ける。
---

# VRC Udon UI Menu

## 役割

ワールド空間 UI とメニュー導線を、VR と Desktop の両方で破綻しにくい形にまとめる。ページ切り替え、閉じる戻る、手持ち UI、Pickup と UI の関係整理を担当する。

## この Skill を使う場面

- ワールド空間 UI を作る
- ページ切り替えや戻る閉じる導線を設計する
- Pickup と UI の干渉を整理したい

## この Skill を使わない場面

- ネットワークの責務や同期仕様を決めるだけの場合: `vrc-udon-networking-rules`
- モジュールの構造設計から始める場合: `vrc-udon-core-module`

## 進め方

1. `references/menu-patterns.md` を読む。
2. UI が固定設置か、手持ちか、Pickup 連動かを最初に決める。
3. 画面遷移は公開メソッド名を揃える。
4. 画面の正本は 1 つの Controller に寄せる。
5. 入力差は Desktop / VR で分けて考える。
6. Networking が絡むなら `$vrc-udon-networking-rules` を追加で読む。

## 担当範囲

- UI レイアウト責務の整理
- 画面遷移イベントの整理
- Pickup と UI の干渉回避
- Desktop / VR 入力差の整理

## 注意点

- 表示状態の正本を複数箇所に持たない
- Pickup Collider と UI Collider の役割を混ぜない
- Close だけで戻れない画面を作らない
- 手持ち UI は姿勢崩れと誤操作を前提にする
