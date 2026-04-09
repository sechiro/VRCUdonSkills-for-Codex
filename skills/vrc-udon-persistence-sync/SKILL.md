---
name: vrc-udon-persistence-sync
description: VRChat UdonSharp で local state、synced state、Player Data、Player Object、Persistence のどれを使うか判断するときに使う。保存単位、復元順、キー設計、同期と保存の責務分離を明示したい場合に使う。ネットワーク全体ルールの固定は `vrc-udon-networking-rules` に分ける。
---

# VRC Udon Persistence Sync

## 役割

UdonSharp システムにおける状態保存と同期の責務を分ける。local state、synced state、Player Data、Player Object、Persistence のどれを使うかを判断し、混線を防ぐ。

## この Skill を使う場面

- local / sync / Player Data / Player Object / Persistence を使い分けたい
- key 設計や復元順を決めたい
- 保存責務と同期責務を分けたい

## この Skill を使わない場面

- networking 文書を先に固定したいだけの場合: `vrc-udon-networking-rules`
- モジュール構造設計から始める場合: `vrc-udon-core-module`

## 進め方

1. `references/sync-decision-guide.md` を読む。
2. Persistence も使うなら `references/persistence-notes.md` を読む。
3. 状態ごとに分類する。
4. 復元順を決める。
5. key 命名規則を決める。
6. Networking 全体の仕様が未整理なら `$vrc-udon-networking-rules` も併用する。

## 担当範囲

- 保存方式の選択
- 復元順の整理
- key 設計の整理
- PlayerObject と Persistence の関係整理

## 注意点

- Persistence はリアルタイム同期の代わりにしない
- 復元前提の値を Awake 相当で即参照しない
- 保存キーは後方互換を考えて決める
- 破棄できる状態まで永続化しない
