# 3D 资产规格说明书：飞鱼 (Flying Fish)

> **英文标识 (ID)**: `flying-fish`  
> **资产层级**: `creatures/flying-fish`  
> **标准规格**: 体长 0.25m, 翼展 0.35m  

---

## 1. 原著设定与生理细节
老人在海中的益友，胸鳍宽大如透明蝉翼，常在海面成群滑翔，指引下方掠食者动向，也会落入小船作为美餐。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D miniature fish model of a tropical flying fish. Slim silver-blue body, dramatically enlarged wing-like translucent pectoral fins with delicate blue veins. Gliding pose, alpha-tested transparent wing texture, under 600 triangles, lightweight Unity Web asset.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
model sheet of flying fish, wings spread wide, top and side view, transparent fin wings, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 600 |
| **骨骼与绑定 (Rigging)** | 4 根骨骼 |
| **贴图槽位 (PBR Maps)** | 256x256 (含 Alpha 半透明通道) |
| **物理碰撞体 (Colliders)** | Sphere Collider |
| **挂点与交互节点 (Sockets)** | Deck_Drop_Socket |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/creatures/flying-fish/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
