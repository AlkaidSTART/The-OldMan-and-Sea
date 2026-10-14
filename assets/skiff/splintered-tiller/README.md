# 3D 资产规格说明书：战损折断尖刺舵柄 (Splintered Tiller Weapon)

> **英文标识 (ID)**: `splintered-tiller`  
> **资产层级**: `skiff/splintered-tiller`  
> **标准规格**: 长 0.55m, 前端爆裂木刺  

---

## 1. 原著设定与生理细节
在午夜群鲨围攻中用力过猛砸断的舵柄残段，前段折断成尖锐参差的木质利矛，沾染鲨鱼黑血与海水，用于反手刺击鲨鱼眼球。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D improvised weapon prop of a snapped broken wooden tiller handle. The front end is shattered into a jagged, sharp spear-like wooden splinter point stained with dried dark blood and seawater. Handgrip wrapped in dirty twine, 350 triangles, WebGL weapon model.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
prop sheet of broken wooden weapon, splintered sharp end, dried blood stains, side profile and perspective, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 380 |
| **骨骼与绑定 (Rigging)** | 静态道具 |
| **贴图槽位 (PBR Maps)** | 512x512 PBR (断木纤维法线与血污遮罩) |
| **物理碰撞体 (Colliders)** | Capsule / Box Collider |
| **挂点与交互节点 (Sockets)** | Hand_Grip, Sharp_Tip_Hit |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/skiff/splintered-tiller/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
