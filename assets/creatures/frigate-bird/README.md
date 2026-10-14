# 3D 资产规格说明书：军舰鸟 (Frigate Bird)

> **英文标识 (ID)**: `frigate-bird`  
> **资产层级**: `creatures/frigate-bird`  
> **标准规格**: 翼展 2.0m, 剪刀尾  

---

## 1. 原著设定与生理细节
高空盘旋的黑色大鸟，视力极佳，低空俯冲指示下方鱼群与鲯鳅动向，是老人的天然空中罗盘。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D soaring bird model, large dark black seabird, long hooked beak, distinct deeply forked scissor tail, wings outstretched gliding pose, 1000 triangles.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
bird model sheet of dark frigate bird, top-down wings spread, forked scissor tail, side profile, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 950 |
| **骨骼与绑定 (Rigging)** | 8 根翅膀与尾翼骨骼 |
| **贴图槽位 (PBR Maps)** | 512x512 PBR |
| **物理碰撞体 (Colliders)** | Trigger |
| **挂点与交互节点 (Sockets)** | Beak_Socket |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/creatures/frigate-bird/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
