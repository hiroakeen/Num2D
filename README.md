# TotalMates

## ゲーム説明
- 2D知育パズル
- 指定された数値と合計値が同じになるように、３つ以上のピースをなぞって足していく
- Unityroomでプレイできます。ランキング対応済み、スマホ可。
  - https://unityroom.com/games/totalmates
- UnityPlayでもプレイできます。
  - https://play.unity.com/en/games/a784c65c-0edf-4176-b58c-7cbb879e0d00/webgl-builds
## こだわったこと
- 子供でもプレイしやすいように暖かいデザインにした
- DoTweenも全体的にかわいらしい動きを採用
- １分の集中で疲れにくく、繰り返しプレイできるようにした。ハイスコア機能も実装
- どうやったら初見でもわかりやすくプレイできるかを考慮
 
## プログラミング技術
- フェードインアウトなどにCanvasGroupを活用
- クラスが単一責任になるよう分割
- AspectKeeperクラスを作成しスマホごとのサイズにも自動調整されるように設定（Expandではなく、外枠は黒くなってサイズ比は一定）

## 制作経緯
- 初日：企画、イシューにやることリストアップ、プロトタイプ制作
  - パズル、短時間カジュアルゲーム、スマホ対応→スワイプ系
  - 数字キャラクター、計算
  - ベースとなるスクリプトの完成（ステート、インターフェイス、rigidbody、コライダー、ピースが消えたらスポーンするなどのロジック）
- ２日目：やることリスト消化
  - 正解不正解のアクション変化
  - aspectkeeperクラス作成
  - スクリプト調整
  - ハイスコア機能playerpref
  - 各シーン作成とシーン遷移
- ３日目：オーディオ、ビルド、Webリリース、バグ修正

## 使用素材（アセット等を記載）
- 効果音：効果音ラボ
- 他：ChatGPTによるイラスト描画、SunoAIによるBGM作曲
