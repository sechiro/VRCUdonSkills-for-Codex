# unity-mcp 利用ルール

## 基本原則

- `unity-mcp` は Editor 支援として使う
- Runtime 依存にはしない
- Builder First の補助として使う
- Unity MCP で見えた結果は、project-owned 文書や Builder / コードへ反映して残す

## 向いている用途

- Scene hierarchy の確認
- Prefab / Asset 状態の確認
- Builder の実行補助
- Editor 設定や package 状態の確認
- Unity Editor 内の差分確認

## 向いていない用途

- Runtime 挙動の断定
- 設計責務そのものを Unity MCP へ押し込むこと
- Networking の正本情報として扱うこと

## 推奨ワークフロー

1. 先に project-owned 文書で仕様を決める。
2. `unity-mcp` は Scene / Prefab / Builder / Editor 状態の確認補助に使う。
3. 得た結果は Builder、コード、文書へ反映して再現可能にする。
4. Unity バージョン差がある可能性を前提に記録を残す。
