# Heavy Runtime Guide

## この reference の目的

- 重い runtime の分割実行パターンをそろえる
- 再構築型システムの考え方を共有する
- reset、replay、undo 風挙動の扱いを明確にする
- UdonSharp 制約下で破綻しにくい処理順をまとめる

## 基本方針

- 重い処理を 1 回で終わらせようとしない
- 正本データを分け、表示や生成物は再構築可能にする
- 途中中断できる構造を持つ
- 変更履歴が必要なら replay 可能な入力列として持つ

## Replay 向きの情報

再適用しやすいもの:

- 操作種別
- 対象 id
- 入力値
- 発生順
- revision counter

## 分割実行の例

段階実行に向くもの:

- batch ごとの適用
- cursor を進める走査
- scheduled event での継続
- cancel や reset を途中で受ける処理

## 注意点

- 正本不明のまま再構築しない
- 途中状態を外へ見せすぎない
- Late Joiner 対応の責務を分ける
- 同期と replay を同一概念として扱わない
- debug 可視化を状態正本にしない
