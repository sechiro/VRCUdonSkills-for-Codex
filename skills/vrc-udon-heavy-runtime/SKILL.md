---
name: vrc-udon-heavy-runtime
description: VRChat UdonSharp で重い再構築処理、replay、reset、段階実行、分割適用が必要な runtime を設計・実装するときに使う。大量オブジェクト更新、操作列の再適用、1 フレームで終わらない処理を安全に扱いたい場合に使う。
---

# VRC Udon Heavy Runtime

## 役割

高負荷になりやすい runtime 処理を、安全に分割しながら扱う。replay、reset、再構築、バッチ適用、カーソル進行などの設計を担当する。

## この Skill を使う場面

- 1 フレームで終わらない処理がある
- replay / reset / rebuild を扱う
- 大量オブジェクト更新や操作列再適用がある

## この Skill を使わない場面

- モジュール構造だけを決めたい場合: `vrc-udon-core-module`
- UI 導線だけを整理したい場合: `vrc-udon-ui-menu`

## 進め方

1. `references/heavy-runtime-guide.md` を読む。
2. 処理を 1 フレームで終える必要があるか確認する。
3. 重いならバッチ、カーソル、段階実行へ分解する。
4. replay 可能な入力列か、再構築可能な正本データかを決める。
5. reset / cancel / rebuild 時の整合性を先に決める。
6. Networking が絡む場合は `$vrc-udon-networking-rules` も併用する。

## 担当範囲

- 重い処理の分割実行
- replay / reset / rebuild の設計
- 正本と生成物の分離

## 注意点

- 全処理を 1 回で流し切る前提にしない
- 正本が無い replay は増やさない
- cancel と reset の意味を分ける
- debug 用の重い可視化を本番ロジックへ混ぜない
