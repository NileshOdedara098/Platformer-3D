# Unity Platformer Runner Game

![Platfomer3D](https://github.com/user-attachments/assets/c7e934b6-c242-4f05-a1e4-0b9589278795)

A 3D endless runner platformer featuring procedural level generation, obstacle systems, and player mechanics.

## Key Features
- **Procedural Platform Generation**: Platforms spawn dynamically in pools
- **Randomized Obstacles**: Each platform has unique obstacle configurations
- **Player Mechanics**: Double jump, movement clamping, and collision detection
- **Dynamic Camera**: Smooth follow camera with rigidbody physics
- **Object Pooling**: Efficient platform management system
- **Ball Projectiles**: Interactive environmental hazards

## Core Scripts Overview

### 1. EnvironmentManager.cs
Manages platform generation and level setup:
- **Platform Pooling**: Creates object pools for 3 platform types
- **Weighted Spawning**: Controls platform frequency using `PlatformXOccurence`
- **Level Initialization**: Positions platforms with `DistanceVector` offsets
- **Start Game Sequence**: Deactivates UI and activates player

### 2. PlayerController.cs
Handles player movement and physics:
- **Movement System**: Arrow/WASD controls with speed adjustment
- **Double Jump**: Spacebar with ground detection
- **Position Clamping**: Restricts player movement on X-axis
- **Collision Handling**: 
  - Ground detection for jump reset
  - Death on obstacle collision
  - Scene reset on falling

### 3. PlatformObstacles.cs
Manages obstacles on platforms:
- **Random Obstacle Activation**: Activates 0-3 obstacles per platform
- **Obstacle Pooling**: Selects from predefined obstacle objects

### 4. CameraFollow.cs
Camera tracking system:
- **Smooth Follow**: Lerp-based movement on Y/Z axes
- **Rigidbody Physics**: Uses physics for camera movement
- **Fixed Offset**: Maintains consistent distance from player

### 5. ShootBall.cs & BallMove.cs
Projectile mechanics:
- **Delayed Activation**: Shoots ball after player passes trigger
- **Physics-Based Movement**: Adds force in specific directions
- **Bounce Mechanics**: Reapplies force on ground collisions

### 6. CoroutineHelper.cs
Coroutine management utility:
- **Optimized Waits**: Cached `WaitForFixedUpdate`/`WaitForEndOfFrame`
- **Time-Based Actions**: `executeInTime` method for animations
- **Singleton Pattern**: Global access via `getInstance()`

## Game Flow
1. EnvironmentManager initializes platform pools
2. PlayerController enables movement and jumping
3. Platforms spawn with randomized obstacles
4. Camera follows player progression
5. Trigger zones activate projectile hazards
6. Player dies on obstacle collision or falling
7. Scene reloads on player death

## Setup Instructions
1. Create player object with Rigidbody and collider
2. Configure platform prefabs with PlatformObstacles component
3. Set up CameraFollow with references to player and camera Rigidbodies
4. Adjust spawn parameters in EnvironmentManager:
   - `DistanceVector` (platform spacing)
   - Platform occurrence weights
   - Pool sizes
5. Configure player constraints in PlayerController:
   - `PlayerXPosClampVector` (horizontal movement limits)
   - Jump force and movement speed

## Optimization Features
- Object pooling for platforms
- Coroutine-based movement processing
- Physics-based interactions
- Weighted random platform selection
