# Fukuro Udon Default Rules

## 目的

Fukuro Udon を導入しているプロジェクトで、特に指定がない場合の既定ルールを固定する。

## 既定ルール

- 物理オブジェクト同期では `VRCObjectSync` より `Manual Sync` を優先する
- `Manual Sync` を使うオブジェクトに `VRCObjectSync` を併用しない
- VR 向け Pickup 設定が必要なら `PickupPlatformOverride` を使う

## 適用対象

- Fukuro Udon 導入済みプロジェクト
- Pickup を伴う物理オブジェクト
- VR 向け操作差分があるギミック

## project-owned 文書へ残すこと

- Fukuro Udon 導入有無
- `Manual Sync` を既定にした理由
- `PickupPlatformOverride` を使う箇所
- 標準設定との差分
