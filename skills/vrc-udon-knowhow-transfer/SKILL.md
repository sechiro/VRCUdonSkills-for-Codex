---
name: vrc-udon-knowhow-transfer
description: Skill 外で import/ export フォルダを使ってノウハウを受け渡す運用を行うプロジェクトで使う。外部参照の取り込み、配布用 know-how の書き出し、どの情報を project-owned へ翻訳するかを決めたい場合に使う。
---

# VRC Udon Knowhow Transfer

## 役割

Skill システム外で `import/` と `export/` を使うノウハウ受け渡し運用を整理する。これは一般的な Udon モジュール構成ではなく、明示的にその運用を採るプロジェクトでだけ使う。

## この Skill を使う場面

- import / export フォルダ運用をプロジェクトで採る
- 外部知見を案件外へ再配布する導線が必要

## この Skill を使わない場面

- 通常の Udon モジュール案件
- project-owned 文書だけで十分な案件

## 進め方

1. `references/transfer-rules.md` を読む。
2. 本当に `import/` または `export/` が必要か確認する。
3. `import/` は外部参照の一時保管に限定する。
4. `export/` は再配布したい know-how だけに限定する。
5. 採用した内容は project-owned 文書へ翻訳して残す。

## 注意点

- デフォルトで常設しない
- imported 情報をそのまま正本にしない
- export には再利用価値があるものだけを置く
- 一時メモと配布物を混ぜない
