# ColorField – Unity Weekend Prototype

Inspired by a post about quick game dev challenges, I built this small but unique prototype in under 24 hours using Unity.  
The main focus was to implement **custom 2D movement physics** where the ball moves strictly along 90° angles and responds predictably to collisions with the field grid.

---

## Gameplay Demo

<p align="center">
  <img src="Assets/Movie_001.gif" alt="Gameplay Demo" width="300"/>
</p>

---

## Features

- Custom rigid movement (no built-in velocity or random bouncing)
- Accurate collision filtering using `includeLayers` and `excludeLayers`
- Grid-based playing field with team-colored cells
- Procedural field generation
- Team identity via separate layers and materials

---

## Technologies Used

- Unity Engine (2D)
- C#
- Rigidbody2D with manual position updates
- Unity Recorder (for gameplay capture)
