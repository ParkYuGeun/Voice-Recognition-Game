
# 🎯 Voice Recognition FlufyBird

> **OpenAi Whisper API를 활용한 음성인식 플러피버드**  
> 게임이 제시한 단어를 정확한 발음으로 말하면 장애물을 피해 정확한 높이로 점프하는 방식의 플러피 버드입니다.

---

## 📸 Demo & Screenshots

| 메인 플레이 화면 |
| :---: | 
|<img src = ./Screenshot/pb-1.PNG width = 600 height = 600 alt = pb-1>|
|<img src = ./Screenshot/pb-2.PNG width = 600 height = 600 alt = pb-2>|
|<img src = ./Screenshot/pb-3.PNG width = 600 height = 600 alt = pb-3>|

---

## ✨ Key Features

- **다양한 적 AI & 웨이브 시스템**: 플레이어를 추적하고 공격하는 적 navMesh AI 패턴 사용
- - **VR 멀미 경감 및 UX**: 횡이동이 아닌 teleport식 이동만을 지원 + snap turn provider을 활용해 vr특유의 멀미 및 어지러움 최소화
- **체감형 VR UI/UX**: 가상 공간 내 3D World Space Canvas를 활용한 체력 및 스코어 표시
- **VR 햅틱 피드백 및 타격감**: xr toolkit이 지원하는 haptic 지원을 이용한 사격, 피격, 장전 시 컨트롤러 진동을 구현하여 손맛 연출.
---
## 💻 Languages and Tools
[![Unity](https://img.shields.io/badge/Unity-000000?style=for-the-badge&logo=unity&logoColor=white)](https://unity.com)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![XR Interaction Toolkit](https://img.shields.io/badge/XR%20Interaction%20Toolkit-6F42C1?style=for-the-badge&logo=unity&logoColor=white)](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@latest)
---

## 🛠 Tech Stack & Environment

| 구분 | 내용 |
| :--- | :--- |
| **Engine** | Unity 2023.3.45f1 |
| **Render Pipeline** | Universal Render Pipeline (URP) |
| **Target Device** | Meta Quest 2 / Quest 3 |
| **SDK / Framework** | Meta XR Core SDK, XR Interaction Toolkit |
| **Language** | C# |
| **IDE** | Visual Studio|

---


## 📁 Project Structure

```text
Assets/
├── Core/               # 핵심 게임 매니저 및 시스템 스크립트
├── Prefabs/            # 총기, 적 AI, UI 프레합
├── Scenes/             # 메인 게임 및 테스트 씬
├── Scripts/            # C# 로직 스크립트
└── Shaders/            # Custom Shader Graph 파일

</div>
