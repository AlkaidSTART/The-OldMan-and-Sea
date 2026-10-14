# 3D 资产规格说明书：僧帽水母 (Portuguese Man-of-War / Agua Mala)

> **英文标识 (ID)**: `portuguese-man-of-war`  
> **资产层级**: `creatures/portuguese-man-of-war`  
> **标准规格**: 气囊 0.3m, 触须延伸 3-5m  

---

## 1. 原著设定与生理细节
如彩虹般华美浮于水面的紫红透明气囊，拖着剧毒刺丝触须，被老人唾弃为虚伪恶毒的婊子，粘连钓绳会灼伤双手。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D marine prop model of a Portuguese Man-of-War jellyfish. Iridescent translucent crest balloon floating on water surface, shifting pink, violet, and electric blue hues. Cluster of delicate tangled dark purple stinging tentacles hanging below. Subsurface scattering transparent glass shader, low-poly optimized, under 1500 triangles.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
concept sheet of Portuguese Man-of-War jellyfish, side view on water line, glowing pink violet float, long poison tentacles, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 1,200 |
| **骨骼与绑定 (Rigging)** | 8 根触须摇摆链 |
| **贴图槽位 (PBR Maps)** | 512x512 (半透明 Alpha / Glass Shader) |
| **物理碰撞体 (Colliders)** | Hazard Trigger Collider (接触造成手部中毒) |
| **挂点与交互节点 (Sockets)** | Float_Apex |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/creatures/portuguese-man-of-war/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
