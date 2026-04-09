---
name: vrc-udon-unity-mcp
description: Unity 2022.3.22f1 前提の VRChat UdonSharp プロジェクトで CoplayDev の unity-mcp を使うときに使う。Scene / Prefab / Builder / Editor 状態の読解や補助操作を Codex から行いたい場合に、Unity MCP を Runtime 依存ではなく Editor 支援として扱うための Skill。
---

# VRC Udon Unity MCP

## 役割

Unity 2022.3.22f1 を使う VRChat UdonSharp 案件で、`unity-mcp` を Editor 支援として安全に扱う。Scene、Prefab、Builder、Package、Editor 状態の確認や補助操作に使い、Runtime の設計責務とは分離する。

## この Skill を使う場面

- プロジェクトが `CoplayDev/unity-mcp` を導入している
- Unity Editor 上の Scene / Prefab / Builder 状態を Codex から読みたい
- Builder First の補助として Editor 側の状態確認や操作支援を使いたい
- Unity 2022.3.22f1 前提で、導入方法と利用範囲も文書化したい

## この Skill を使わない場面

- `unity-mcp` を導入していない
- Runtime 実装や UdonSharp コードだけで完結する
- モジュール構造だけ整理したい

## 前提

- Unity バージョンは `2022.3.22f1`
- `unity-mcp` は Runtime 依存ではなく Editor 支援ツールとして扱う
- Scene / Prefab / Builder / Editor 設定の読解と補助に使う
- 接続先は導入先プロジェクト直下の `.mcp.json` に書く
- 実装や仕様の正本は project-owned 文書とコード側に残す

## 進め方

1. `references/unity-mcp-install-and-scope.md` を読む。
2. `references/unity-mcp-usage-rules.md` を読む。
3. Unity が `2022.3.22f1` であることを確認する。
4. 導入先プロジェクト直下に `.mcp.json` を置き、接続先を書く。
5. 導入先の `docs/ENVIRONMENT_SETUP.md` や `docs/SYSTEM_SPEC.md` に、導入方法と利用範囲を書く。
6. Scene / Prefab / Builder の確認補助に使う。
7. Unity MCP で得た結果は Builder、コード、project-owned 文書へ反映して再現可能にする。

## 併用ガイド

- 文書と環境再現は `$vrc-udon-project-docs`
- モジュール構造は `$vrc-udon-core-module`
- UI と操作導線は `$vrc-udon-ui-menu`
- networking 文書は `$vrc-udon-networking-rules`

## 注意点

- `unity-mcp` を Runtime 依存として扱わない
- Unity Editor 上で見えた状態を、そのままワールド実行時の保証とみなさない
- `.mcp.json` の接続先は project root に置き、案件ごとに管理する
- 導入方法、利用範囲、無効化条件を project-owned 文書へ残す
- Unity バージョン差がある前提で扱う
