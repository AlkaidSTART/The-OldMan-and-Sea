# 3D 资产规格说明书：绑刀船桨临时长矛 (Knife on Oar Spear)

> **英文标识 (ID)**: `knife-on-oar-spear`  
> **资产层级**: `items/knife-on-oar-spear`  
> **标准规格**: 全长 2.8m, 刀刃外露 0.15m  

---

## 1. 原著设定与生理细节
鱼叉丢失后老人的临时发明：将水手折刀刀刃用生皮带与钓索五花大绑在木桨叶尖端制成长柄刺矛，第二波防卫角鲨的核心兵器。

---

## 2. AI 3D 生成提示词 (Prompt Library)

### A. Text-to-3D 指令 (适用 Meshy / Tripo3D / Rodin / CSM / Luma Genie)
```text
3D makeshift weapon prop, a sailor's folding pocketknife with exposed blade lashed firmly to the blade tip of a long wooden oar using crisscrossed raw leather thongs and green fishing cord. Improvised ocean spear, rugged survivalist design, 600 triangles, clean PBR textures.
```

### B. 正交三视图生成 Prompt (适用 Midjourney v6 / SDXL - Image-to-3D 预处理)
```text
prop sheet of an improvised spear, folding knife bound to an oar tip with cord, detailed strapping, side and perspective view, white background. --ar 16:9 --v 6.0
```

---

## 3. Unity WebGL 工业级规格标准

| 规范项目 | 参数要求 |
| :--- | :--- |
| **三角面预算 (Triangles)** | 650 |
| **骨骼与绑定 (Rigging)** | 静态 |
| **贴图槽位 (PBR Maps)** | 512x512 PBR |
| **物理碰撞体 (Colliders)** | Capsule Collider |
| **挂点与交互节点 (Sockets)** | Double_Hand_Grip, Blade_Stab_Point |

---

## 4. 导入与组件挂载指引 (Unity Prefab Setup)
1. **模型导入**：将生成的 `.fbx` 或 `.glb` 文件放置在 Unity 项目的 `Assets/Models/items/knife-on-oar-spear/` 下。
2. **材质配置**：使用 Unity URP Lit Shader，贴图通道开启半精度压缩以适配 WebGL 内存优化。
3. **节点对齐**：确保交互挂点（Sockets）与角色手部或船体结构精确对齐。
