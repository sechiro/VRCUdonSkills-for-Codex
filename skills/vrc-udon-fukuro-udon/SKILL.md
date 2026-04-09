---
name: vrc-udon-fukuro-udon
description: Fukuro Udon を導入している VRChat UdonSharp プロジェクトで使う。特に指定がない場合に、VRC Object Sync の代わりに Manual Sync を優先し、PickupPlatformOverride による VR 向け Pickup 上書き設定を使う前提を適用したいときに使う。通常案件では使わない。
---

# VRC Udon Fukuro Udon

## 役割

Fukuro Udon を導入している案件でだけ使う前提差分をまとめる。通常の UdonSharp ルールに追加して、同期方式と Pickup 設定を Fukuro Udon 前提へ寄せる。

## この Skill を使う場面

- プロジェクトが Fukuro Udon を導入している
- 特に指定がない場合の既定動作を Fukuro Udon 前提にしたい
- Pickup 系の実装で VR 向け上書き設定を使う

## この Skill を使わない場面

- Fukuro Udon を導入していない案件
- 通常の UdonSharp / VRC SDK 標準前提で十分な案件

## 既定ルール

- 特に指定がなければ、`VRCObjectSync` の代わりに `Manual Sync` を使う
- `Manual Sync` を使うオブジェクトには `VRCObjectSync` を併用しない
- VR 向け Pickup 上書き設定が必要なら `PickupPlatformOverride` を使う
- Fukuro Udon 前提でも、project-owned 文書へ採用理由を残す

## 進め方

1. `references/fukuro-default-rules.md` を読む。
2. Fukuro Udon 導入有無を確認する。
3. Pickup / 物理同期オブジェクトで、`Manual Sync` を既定にするか確認する。
4. VR 向け Pickup 調整が必要なら `PickupPlatformOverride` を採用する。
5. 導入先の `docs/SYSTEM_SPEC.md` と必要なら `docs/NETWORKING_RULES.md` に前提を書く。

## 併用ガイド

- 文書土台は `$vrc-udon-project-docs`
- モジュール構造は `$vrc-udon-core-module`
- networking 文書は `$vrc-udon-networking-rules`
- UI / Pickup 導線は `$vrc-udon-ui-menu`

## 注意点

- Fukuro Udon 前提を通常案件へ持ち込まない
- `Manual Sync` と `VRCObjectSync` の二重適用をしない
- PickupPlatformOverride の採用理由を文書化する
