# 3D 资产规格说明书：大蠵龟 (Loggerhead Sea Turtle)

> **英文标识 (ID)**: `loggerhead-turtle`  
> **资产层级**: `creatures/loggerhead-turtle`  
> **标准规格**: 背甲长 1.1m, 宽 0.8m  

---

## 1. 原著设定与生理细节
老人在深海偶遇的笨拙朋友，啃食黄色马尾藻与水母，眼神温和，带给孤独老人心灵抚慰。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D sea turtle model, rough brownish-yellow textured carapace shell, paddle flippers with blunt claws, ancient wrinkled neck and head, slow swimming pose, 2500 triangles, PBR textures.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
sea turtle model sheet, top carapace view, side profile, paddle flippers, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 2,200 |
| **骨骼与绑定 (Rigging)** | 14 根骨骼 |
| **贴图槽位 (PBR Maps)** | 512x512 PBR |
| **物理碰撞体 (Colliders)** | Box Collider |
| **挂点与交互节点 (Sockets)** | Shell_Mount |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/creatures/loggerhead-turtle/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
