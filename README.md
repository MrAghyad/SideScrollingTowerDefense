# Day 1: Player Controller
The tutorial of day one revolves around a comprehensive set of scripts designed to handle various aspects of a 2D game, including animation, camera movement, resource management, and player interactions, movements and physics. The game features a player character who can interact with resources, collect them, and bring them to a designated area (castle). The environment dynamically spawns resources, and the player can chop or build using these resources.



## Key Features 🌠
### Animation Handler (```AnimationHandler.cs```)
- Manages character animations using an Animator component.
- Allows switching between animation states through an integer parameter.

### Camera Follow (```CameraFollow.cs```)
* Smoothly follows a target (player) with an adjustable offset and damping.

### Damageable Entities (```Damageable.cs```)
- Implements health management for characters and objects.
- Deals with damage reception and updates health bars accordingly.
- Handles destruction logic for GameObjects and their components.

### Resource Management (```GameResource.cs```, ```GameResourceUnit.cs```)
- Manages in-game resources (e.g., wood, meat) with types, values, and animations.
- Resources react to damage and can spawn resource units upon destruction.
- Resource units have health and value properties, and can interact with the ground.

### Player Mechanics (```Player.cs```)
- Manages player state, movement, and animations.
- Handles resource collection, carrying, and resource interaction logic.
- Responds to keyboard inputs for player control.

### Player's Tool Interaction (```PlayerAxe.cs```)
- Enables the player's axe to interact with resources, applying damage.

### Castle Interaction (```PlayerCastle.cs```)
- Manages resource delivery to the castle and updates resource counts.

### Dynamic Resource Spawning (```ResourcesSpawner.cs```)
Spawns resources at random intervals within a designated area.

## Clone The Project and Get Started 🔥
Run the following commands <br/>
```❯ git clone git@github.com:MrAghyad/SideScrollingTowerDefense.git```<br/>

```❯ git checkout day-1```<br/>

Or use github desktop to clone the project and switch the branch to ```day-1```

## Test The Implementation 😃
The whole implementation can be tested in scene ```PlayerControllerScene``` 🔥🚀🎮

## Recommended Excersise 👓
- Try to recreate the mentioned scene (```PlayerControllerScene```) and its core objects.
- Split sprites and create animations (we have multiple spritesheets to play with)
- Read the player script and understand how it is working
- Check out the animations of each object (Player [Pawn], Tree, Wood, Sheep, and Meat)
- Re-implement the animations and player behavior
- Check out other scripts that control the camera, resources, and other objects

## Need Support ❔
Reach out through [linkedin](https://www.linkedin.com/in/aghyad-albaghajati/) or [X/Twitter](https://twitter.com/Mr_Aghyad)
