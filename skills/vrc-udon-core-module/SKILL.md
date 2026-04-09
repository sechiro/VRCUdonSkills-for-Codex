---
name: vrc-udon-core-module
description: VRChat UdonSharp の再利用モジュールを Builder First で立ち上げるときに使う。Runtime と Editor の責務分離、project-owned なドキュメント整備、シーン生成前提の構成、外部プロジェクトへ持ち出せる基本骨格が必要な場合に使う。文書雛形の配布そのものは `vrc-udon-project-docs`、ネットワーク仕様の固定は `vrc-udon-networking-rules` に分ける。
---

# VRC Udon Core Module

## 役割

VRChat UdonSharp の再利用可能な基本モジュールを組み立てる。特に Builder First、Runtime / Editor 分離、project-owned な仕様書整備を前提にした初期構成を作る。

## この Skill を最初に使う場面

- 新しい UdonSharp モジュールを切り出す
- Builder と Runtime の責務を分けたい
- 外部プロジェクトへ持ち出せる基本骨格を作りたい

## この Skill を使わない場面

- 文書雛形だけを先に配りたい場合: `vrc-udon-project-docs`
- ネットワーク仕様だけを決めたい場合: `vrc-udon-networking-rules`
- Persistence の方式選定だけをしたい場合: `vrc-udon-persistence-sync`

## 進め方

1. 最初に `references/core-rules.md` を読む。
2. Builder を含む構成を作る場合は `references/module-workflow.md` も読む。
3. まず責務を分ける。
   - Runtime は `Scripts/` または `Runtime/`
   - Editor 拡張は `Editor/`
   - プロジェクト固有仕様は `docs/`
4. 導入先に基本文書が無い場合は `$vrc-udon-project-docs` を併用する。
5. Sync、Ownership、Network Event、Late Joiner、Persistence が絡む場合は `$vrc-udon-networking-rules` を併用する。
6. Builder First を維持する。
7. Runtime の UdonSharpBehaviour は小さく保つ。
8. 他プロジェクトへ配布する前提でも、プロジェクト固有要件は project-owned ドキュメントに逃がす。

## 担当範囲

- モジュールの基本構成決定
- Builder の責務分離
- Runtime の責務分離
- 文書の最低ライン整理
- 外部プロジェクトへ持ち出す前提の骨格化

## 担当しない範囲

- Udon の個別ロジックを詳細実装すること
- Unity MCP など個別機能の Runtime 実装を直接決め打ちすること
- imported 資料の永続運用を常設前提にすること
- ネットワーク仕様を単独で確定すること
