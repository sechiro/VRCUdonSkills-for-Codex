# Module Workflow

## 目的

Builder First な UdonSharp モジュールを外部プロジェクトへ導入しやすい形で作るときの手順をまとめる。

## 手順

1. まずモジュール境界を決める。
   - 何を入力として受けるか
   - 何を Scene に生成するか
   - 何を Runtime が保持するか
2. `docs/SYSTEM_SPEC.md` に責務を書く。
3. Builder が階層を生成するなら `docs/SCENE_OBJECT_SPEC.md` を書く。
4. Runtime 側は UdonSharpBehaviour を小さく分ける。
5. Builder 側で参照接続、命名、初期配置を済ませる。
6. Networking が必要なら `$vrc-udon-networking-rules` を併用してルールを追加する。
7. 導入先へ配布するときは、プロジェクト固有部分を docs に残し、再利用ロジックと混ぜない。

## 判断基準

- Scene の正本が手作業なら Builder を増やしすぎない
- 参照配線が複雑なら Builder に寄せる
- ランタイム中に再構築しないものは Editor 側で確定する
- データ正本が曖昧なら先に仕様書を直す

## 完了条件

- Runtime / Editor の責務が説明できる
- Scene 再生成時の前提が残っている
- 導入先で最低限の文書と構成が再現できる
