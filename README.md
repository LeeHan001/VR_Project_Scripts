# VR Project Scripts

Unity XR を用いて制作した **1人開発のVRゲームプロジェクト** における主要 C# スクリプトをまとめたリポジトリです。  
プレイヤー移動、オブジェクト相互作用、戦闘、パズル、探索、敵AIなど、VRゲームプレイの中心ロジックを実装しました。

## Demo
- YouTube Video: https://www.youtube.com/watch?v=IKedDl08ukM&t=184s
- Reference URL : https://gossamer-duck-07e.notion.site/VR-PROJECT-Unity-3D-VR-Game-1-1ec79bfcde0480d1ad6fcba3d08f93aa

---

## 概要

本プロジェクトは、Unity XR 環境を活用し、没入感のある複数マップ型 VR ゲームを制作したものです。  
プレイヤーの移動、武器操作、敵との戦闘、オブジェクト挿入型パズル、探索ミッションを組み合わせ、VRならではの体験を目指しました。

---

## 主な実装内容

### 1. XR移動システム
XR Origin を用いて、以下の基本操作を実装しました。

- Teleport
- Move
- Snap Turn

また、メインマップから複数のサブマップへ遷移できる構成を設計しました。

### 2. マップ1：シューティング＆スポーンシステム
- XR Grab Interactable を用いた武器の把持
- Raycast方式と弾丸生成方式の両方に対応した射撃処理
- 時間経過に応じてスポーン速度が上昇する Spawner システム
- 一定条件でボス出現イベントが発生する進行制御

### 3. マップ2：パズル型インタラクション
- Socket Interactor を利用したオブジェクト挿入処理
- 複数の雪玉を正しい位置に配置して雪だるまを完成させるパズル
- Particle System による雪の演出追加

### 4. マップ3：探索型ミッション
- Terrain を利用した地形・森林マップ制作
- 複数の鍵を集めることでメインキーを生成
- 特定の鍵を使ってドアを開けるクリア構成
- Socket Interactor による物理的な鍵挿入表現

### 5. マップ4：AI追跡戦闘
- NavMeshAgent を利用した敵AIの追跡処理
- スポナーからのランダム生成
- HP UI と連動したゲームオーバー／クリア処理

---

## 工夫した点

### VR向けUI表示の改善
通常のPCゲームのように画面端へUIを固定すると、VRでは視線移動が増えて疲労感が高くなり、没入感も低下します。  
そこで、左手コントローラにUIを追従配置し、**視線を一定時間向けた場合のみUIを表示する仕組み**を実装しました。

- カメラ正面ベクトルと対象方向ベクトルの角度を比較
- 一定角度以内を一定時間維持した場合のみ表示
- 視線が外れた瞬間に非表示化

これにより、必要な時だけUIを確認できる設計にし、VR体験の快適性を高めました。

---

## 主要スクリプト例

- `ActivateOnLookAt.cs`  
  視線方向を判定し、左手コントローラUIの表示／非表示を制御

- `MonsterAI.cs`  
  NavMeshAgent を利用した敵追跡ロジック

- `MonsterSpawner.cs` / `Spawner.cs`  
  敵やイベントオブジェクトの生成管理

- `SceneChanger.cs`  
  メインマップと各サブマップ間の遷移制御

- `DoorManager.cs`  
  鍵条件に応じたドア開閉処理

- `BGMManager.cs`  
  シーン進行に応じたBGM制御

- `Shooter` 関連スクリプト  
  Raycast射撃、弾丸生成、ヒット処理

---

## 技術要素

- Unity
- C#
- XR Origin
- XR Grab Interactable
- Socket Interactor
- NavMeshAgent
- Terrain
- Particle System

---

## 担当内容

- ゲーム全体の企画・設計
- マップ構成設計
- XR移動・相互作用実装
- 敵AI・スポーンシステム実装
- 射撃システム実装
- パズル・探索ロジック実装
- VR向けUI表示改善ロジック実装

---

## 補足

使用アセットは一部のモデリングと Terrain を中心としており、  
それ以外のスクリプトおよび演出処理は自作しています。
