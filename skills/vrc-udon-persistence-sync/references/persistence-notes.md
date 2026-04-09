# Persistence Notes

## この reference の目的

- Persistence を導入する前に前提をそろえる
- Player Data と in-instance sync を混同しない
- 復元順と key 設計の注意点を共有する

## 外部参照

- 参照日: 2026-04-07
- 記事: `VRChat Persistence完全ガイド【クリエイター向け】`
- URL: `https://vrcprog.hatenablog.jp/entry/guide-persistence-for-creators#Player-Data`

## サポートツール候補

- 参照日: 2026-04-07
- ツール: `VRCNetworkIDGuard`
- URL: `https://github.com/tktcorporation/VRCNetworkIDGuard`
- 用途: Network ID の変更検知、Pinned ファイルによる復元、CI での整合性チェック

## 採用した整理

- Persistence はセッションをまたぐ保存の話として扱う
- Player Data はプレイヤー単位の保存責務として扱う
- 復元順が未定のまま実装を始めない
- key 設計は将来の追加や互換性を前提に決める

## 注意点

- リアルタイム同期が必要な値は別に設計する
- 保存値の欠損時デフォルトを決めておく
- バージョン違いのデータが来る前提で読む
- 外部記事の内容は公式仕様と実測を分けて扱う
- PlayerObject が保存対象や復元対象に関わる場合は、関連する Network ID の変更を移行計画なしで行わない
