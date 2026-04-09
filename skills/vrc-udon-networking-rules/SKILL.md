---
name: vrc-udon-networking-rules
description: VRChat UdonSharp で Sync、Ownership、Network Event、Late Joiner、Player Data、Player Object を扱う設計や `docs/NETWORKING_RULES.md` の整備が必要なときに使う。Persistence を含むネットワーク仕様を project-owned に明文化したい場合にも使う。保存方式の選定そのものは `vrc-udon-persistence-sync` に分ける。
---

# VRC Udon Networking Rules

## 役割

VRChat UdonSharp のネットワーク挙動を、実装前に `docs/NETWORKING_RULES.md` として固定する。誰が状態を持つか、何を同期するか、何をイベントで流すか、Late Joiner をどう扱うかを先に決める。

## この Skill を使う場面

- synced variable を使う
- `SendCustomNetworkEvent` を使う
- Ownership の移譲や検証がある
- Late Joiner 対応が必要
- Player Data や Player Object を含むネットワーク仕様を project-owned 文書へ固定したい

## この Skill を使わない場面

- 保存方式だけを選びたい場合: `vrc-udon-persistence-sync`
- UI や操作導線だけを設計したい場合: `vrc-udon-ui-menu`

## 進め方

1. `references/networking-guide.md` を読む。
2. 導入先に networking ルール文書が無ければ `assets/templates/docs/NETWORKING_RULES.md` を配置する。
3. 仕様を次の順で埋める。
   - 状態正本
   - イベント
   - Ownership
   - Late Joiner
   - Persistence を含む復元順
4. 実装前に `docs/NETWORKING_RULES.md` を確定する。

## 担当範囲

- Network Event と同期変数の役割分離
- Ownership の固定
- Late Joiner 前提の仕様化
- networking 文書雛形の配布

## 注意点

- 一時イベントと持続状態を混ぜない
- 誰が最終状態を確定するかを 1 箇所に寄せる
- Persistence は in-instance sync の代替ではない
- 公式根拠が薄い挙動は断定しない
