---
name: vrc-udon-data-lists
description: VRChat UdonSharp で DataList、Data Lists、Data Containers を明示的に使うよう求められたときだけ使う。配列より DataList を選ぶ理由、利用範囲、更新方針、代替不可な事情を整理したい場合に使う。
---

# VRC Udon Data Lists

## 役割

DataList を使うと明示された案件だけで、DataList 前提の設計と実装方針を与える。明示がない案件では使わない。

## この Skill を使う場面

- ユーザーが DataList / Data Containers を明示した
- 既存プロジェクトが DataList 前提で組まれている

## この Skill を使わない場面

- 通常案件
- 配列で十分な状態管理

## 使い方

1. まず `references/datalist-rules.md` を読む。
2. ユーザーが `DataList`、`Data Lists`、`Data Containers` のいずれかを明示したか確認する。
3. 明示がない場合はこの Skill を使わず、固定長配列を優先する。
4. 明示がある場合だけ、DataList を使う理由と範囲を文書化する。
5. 既存プロジェクトがすでに DataList 前提なら、その前提に合わせて実装する。

## 注意点

- デフォルト採用しない
- なんとなく便利そう、では選ばない
- 配列で足りるなら配列を使う
- DataList の正本と変換コストを意識する
