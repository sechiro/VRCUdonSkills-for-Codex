# Doc Pack Guide

## 目的

外部プロジェクトへ入れる基本文書の役割分担をそろえる。

## 各文書の役割

### AGENTS.md

- 読み順を示す
- 実装前に何を読むべきかを書く
- Skill 依存ではなく project-owned な入口にする

### README.md

- プロジェクトの概要を書く
- 主要フォルダの意味を書く
- どの文書を見ればよいかを示す

### .mcp.json

- Unity MCP を使う案件でだけ置く
- 接続先を project root で管理する
- Runtime ではなく Editor 支援の設定として扱う

### docs/SYSTEM_SPEC.md

- システム全体の責務を書く
- モジュール境界を書く
- 正本データと更新経路を書く

### docs/SCENE_OBJECT_SPEC.md

- Builder 生成物の階層規則を書く
- 命名規則を書く
- 手修正してよい範囲と再生成前提を分ける

### docs/ENVIRONMENT_SETUP.md

- Unity / SDK / UdonSharp の前提を書く
- セットアップ手順を書く
- 検証方法と再現条件を書く
- 必要なら Unity MCP の導入と `.mcp.json` 配置も書く

### docs/OFFICIAL_REFERENCE_MAP.md

- 公式ドキュメントの参照先をまとめる
- どのテーマでどの公式ページを見るかを示す
- 外部補助資料との使い分けを書く

### docs/UdonSharpProgramAsset_AutoGeneration.md

- U# Program Asset 自動生成の導入手順を書く
- 対象フォルダ限定のルールを書く
- Editor スクリプトの配置先と確認方法を書く

## 運用ルール

- 実装が変わったら project-owned 文書も更新する
- imported 参照は原文のまま置かず、採用した内容だけを project-owned 文書へ移す
- Networking が必要なら別途 `$vrc-udon-networking-rules` を使う
- 環境差や参照先差が出るなら `ENVIRONMENT_SETUP` と `OFFICIAL_REFERENCE_MAP` を先に更新する
- Editor 自動化を入れる場合は、対象範囲と配置先を文書に残す
- Unity MCP を使う場合は `.mcp.json` を project root に置く前提を明記する
