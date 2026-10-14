# 3D 资产规格说明书：渔童马诺林 (Manolin)

> **英文标识 (ID)**: `manolin`  
> **资产层级**: `characters/manolin`  
> **标准规格**: 高度 1.62m, 少年体态  

---

## 1. 原著设定与生理细节
忠诚陪伴老人的古巴少年，目光清澈热忱，身穿简朴无袖棉布背心与卷边短裤，皮肤被海风吹成健康小麦色，赤足。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D low-poly character model of a young 15-year-old Cuban fisher boy, Manolin. Youthful sun-kissed tan skin, friendly determined dark eyes, messy black short hair. Wearing a simple beige sleeveless cotton work shirt, faded navy blue shorts, barefoot. Athletic lean build, clean topology, A-pose, game ready character asset for Unity WebGL, hand-painted PBR texture, neutral lighting.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
character turnaround sheet, front view, side view, back view of Manolin, a young Cuban fisher boy in A-pose. White background, orthographic camera, clean lines, wearing a loose tan vest and dark rolled shorts, barefoot. 3D model reference sheet, symmetrical, stylized realism, Unreal Engine 5 aesthetic. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 8,000 - 10,000 |
| **骨骼与绑定 (Rigging)** | 52 根 Unity Humanoid 骨骼 |
| **贴图槽位 (PBR Maps)** | 1024x1024 PBR |
| **物理碰撞体 (Colliders)** | Capsule Collider |
| **挂点与交互节点 (Sockets)** | RightHand_Socket (提咖啡罐/送饵篮) |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/characters/manolin/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
