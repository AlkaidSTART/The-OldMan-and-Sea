# 3D 资产规格说明书：灰鲭鲨 / 尖吻鲭鲨 (Mako / Dentuso)

> **英文标识 (ID)**: `mako-shark`  
> **资产层级**: `creatures/mako-shark`  
> **标准规格**: 体长 3.2m, 鱼雷型极速捕食者  

---

## 1. 原著设定与生理细节
深青蓝色金属背脊、雪白腹部；口中有八排向内弯曲如人爪般尖利的利齿；游速极快，嗅血而来直冲马林鱼肉。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D creature model of an apex predator Mako shark (Dentuso). Streamlined hydrodynamic torpedo body, vibrant metallic deep-blue back, crisp white underbelly. Jaws slightly open displaying rows of curved inward claw-like razor-sharp white teeth. Dead black predatory eyes, tall sharp dorsal fin, symmetrical swimming T-pose, clean edge loop topology for spine flexing, low-poly Unity WebGL game asset, PBR material.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
creature model sheet of a Mako shark, showing side profile, top-down view, and front-facing open mouth view. Deep blue metallic back, pure white belly, claw teeth visible, orthographic camera, neutral background, 3D model reference sheet, photorealistic texture guide. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 6,000 - 8,000 |
| **骨骼与绑定 (Rigging)** | 20 根骨骼 (脊柱链 + 下颌) |
| **贴图槽位 (PBR Maps)** | 1024x1024 PBR (高光湿润水膜感) |
| **物理碰撞体 (Colliders)** | Capsule Collider + 头部弱点 Trigger Collider (Brain Spot) |
| **挂点与交互节点 (Sockets)** | Bite_Target_Socket |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/creatures/mako-shark/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
