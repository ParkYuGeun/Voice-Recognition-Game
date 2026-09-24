
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

- **OpenAI Whisper 기반 실시간 음성 인식 제어**: 마이크 입력 오디오 스트림을 처리하고 OpenAI Whisper API와 연동하여 플레이어의 목소리(음성 명령/음량)를 실시간으로 분석해 캐릭터 점프 및 이동을 제어하는 이색 컨트롤 메커니즘 구현
- - **오디오 버퍼링 & API 지연 시간(Latency) 최적화**: 실시간 게임 환경에 맞춰 입력 오디오 데이터를 효율적으로 쪼개고(Chunking) 버퍼링하여, API 호출 응답 속도 및 네트워크 지연으로 인한 조작감 저하를 최소화
- **음성 기반 물리 엔진 연동**: Whisper가 인식한 입력 신호를 Unity Rigidbody2D 물리 엔진의 AddForce/Velocity 연산과 매핑하여 자연스러운 점프 및 낙하 물리 연출 구현
- **음성 데이터 전처리 및 노이즈 필터링**: 마이크 입력 신호의 데시벨(dB) 및 주파수 범위를 감지/전처리하여 주변 배경 소음으로 인한 오작동을 방지하고 정확한 점프 명령만 식별하도록 설계
---
## 💻 Languages and Tools
[![Unity](https://img.shields.io/badge/Unity-000000?style=for-the-badge&logo=unity&logoColor=white)](https://unity.com)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![OpenAI Whisper](https://img.shields.io/badge/OpenAI%20Whisper-412991?style=for-the-badge&logo=openai&logoColor=white)](https://openai.com/research/whisper)

## 🛠 Tech Stack & Environment

| 구분 | 내용 |
| :--- | :--- |
| **Engine** | Unity 2023.3.45f1 |
| **Render Pipeline** | URP 2D Renderer |
| **Target Device** | PC |
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
