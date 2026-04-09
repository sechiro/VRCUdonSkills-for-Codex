---
name: codex-edit-stability-windows
description: Windows 環境で Codex がファイル編集を行うときに、文字化け、BOM 混入、PowerShell の here-string 展開事故、apply_patch 失敗、意図しないエンコーディング変換を避けたい場合に使う。Codex 自体の編集運用を安定させるための補助 Skill であり、実装対象ドメインではなく編集手段の選択、検証、復旧手順を整理したいときに使う。
---

# Codex Edit Stability Windows

## 役割

Windows 上での Codex 編集作業を安定させる。特に `apply_patch` 失敗、PowerShell 由来の文字化け、UTF-8 / BOM 問題、here-string 展開事故を避けるための作業手順を与える。

## この Skill を使う場面

- Codex の編集で文字化けが起きやすい
- `apply_patch` が頻繁に失敗する
- PowerShell の `Set-Content` / here-string で内容が壊れやすい
- 日本語を含む Markdown やテンプレートを安全に編集したい
- 編集後の検証手順を標準化したい

## この Skill を使わない場面

- 通常の実装ノウハウだけで十分なとき
- Windows 特有の編集事故を扱わないとき

## 基本方針

1. まず `references/edit-failure-analysis.md` を読む。
2. 実編集前に `references/safe-edit-workflow.md` を読む。
3. 文字化けしやすいファイルでは、PowerShell の安易な文字列置換より安全な手段を優先する。
4. `apply_patch` が通るなら最優先で使う。
5. `apply_patch` が使えない場合だけ、UTF-8 を明示した安全な書き込みへ切り替える。
6. 編集後は必ず `references/verification-checklist.md` の確認を行う。

## 優先する編集手段

1. `apply_patch`
2. 既存の安全なテンプレートコピー
3. UTF-8 を明示した最小限のファイル再書き込み

## 避けるもの

- PowerShell のバッククォート解釈が絡む複雑な置換
- 展開付き here-string 内での Markdown / URL / バッククォート混在
- 文字コードを明示しない `Set-Content`
- BOM 混入を意識しない C# / YAML 出力
- 編集後未検証のまま完了扱いにすること

## 典型フロー

1. 失敗原因が `apply_patch` か文字化けかを切り分ける。
2. 対象ファイルが `.cs` / `.yaml` / `.md` のどれかを確認する。
3. `apply_patch` を試す。
4. 失敗したら、ファイル種別に応じた安全手段へ切り替える。
5. 編集後に、置換文字、BOM、意図しない `?`、未解決プレースホルダを確認する。
6. その後にステージングや次作業へ進む。
