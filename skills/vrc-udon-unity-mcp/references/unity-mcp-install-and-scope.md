# unity-mcp 導入と対象範囲

## 対象

- Unity: `2022.3.22f1`
- ツール: `CoplayDev/unity-mcp`
- 用途: Unity Editor 上の補助

## 参照した公開情報

- GitHub: `https://github.com/CoplayDev/unity-mcp`
- README の Quick Start に、Unity package の Git URL 導入方法がある
- Git URL: `https://github.com/CoplayDev/unity-mcp.git?path=/MCPForUnity#main`

## 確認できた前提

- README では `Unity 2021.3 LTS+` を前提としている
- `Unity 2022.3.22f1` はその範囲に含まれる
- Unity 側の入口は `Window > MCP for Unity`
- Unity Editor と MCP クライアントの橋渡しとして使う

## project-owned 文書へ残す内容

- Unity バージョン
- 導入方法
- 利用する MCP クライアント
- project root の `.mcp.json` に書く接続先
- 何に使うか
- 何には使わないか

## 接続設定

接続先は、導入先プロジェクト直下の `.mcp.json` に書く前提で扱う。

デフォルト設定例:

```json
{
  "mcpServers": {
    "UnityMCP": {
      "type": "http",
      "url": "http://127.0.0.1:8080/mcp"
    }
  }
}
```

## この Skill で想定する用途

- Scene 状態の確認
- Prefab / Asset 状態の確認
- Builder 実行支援
- Package / Editor 設定の確認補助

## この Skill で想定しない用途

- Runtime 挙動の保証
- UdonSharp 制約の回避
- Networking や Persistence の正本判断
