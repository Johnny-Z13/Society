# Society Project

A Unity-based AI agent society simulation using AG2 (AutoGen) for agent behaviors and decision making.

reference : https://github.com/ag2ai/ag2/blob/main/README.md

## Current Implementation Status
- **Primary Framework**: Using AG2 (AutoGen) for all agent behaviors and decision making
- **Genagents**: Currently maintained as reference material only. Not actively used in current implementation. README will be updated if/when Genagents features are integrated.

## Project Structure

```
Society/
├── Assets/               # Unity project files
│   ├── Materials/       # Agent and environment materials
│   ├── Prefabs/        # Agent prefabs and reusable objects
│   ├── Scenes/         # Unity scenes
│   └── Scripts/        # C# scripts for Unity
│       ├── AG2Agent.cs    # Individual agent behavior
│       └── AgentManager.cs # Agent system management
├── src/                 # Python backend
│   ├── unity_interface/ # Unity-Python communication
│   └── agents/         # AG2 agent definitions
├── tests/              # Python tests
└── genagents/          # Reference only - potential future integration
```

## Technologies Used

### Active
- Unity 2022.3 LTS
- Python 3.8+
- AG2 (AutoGen) for AI agent behaviors
- NativeWebSocket for Unity-Python communication

### Reference Only
- Genagents (maintained for future potential integration)

## Setup

1. Clone the repository:
```bash
git clone https://github.com/Johnny-Z13/Society.git
```

2. Set up Python environment:
```bash
cd Society
python -m venv venv
source venv/Scripts/activate  # Windows: venv\Scripts\activate
pip install -r requirements
```

3. Configure OpenAI API:
- Create OAI_CONFIG_LIST file with your API key
- Never commit this file (it's in .gitignore)

4. Open Unity project:
- Open Unity Hub
- Add project from Society folder
- Open project

## Development

### Current Architecture
- Agent behavior is managed through AG2 in Python
- Unity provides visualization and physics simulation
- Communication happens via WebSocket
- Agents can interact, make decisions, and evolve based on their environment

### For Contributors
- Focus development on AG2 integration
- Any Genagents integration must be discussed and documented before implementation
- Keep this README updated with any framework changes

## Running the Project

1. Start the Python server:
```bash
python tests/test_server.py
```

2. Open Unity and run the scene

## Contributing
1. All new features should primarily use AG2 framework
2. Create feature branches from main
3. Update documentation with any changes
4. Submit pull requests with clear descriptions

## License

[Add license information]

## Notes for Collaborators
- This project actively uses AG2 for agent behaviors
- Genagents is maintained as reference material only
- Any changes to this architectural decision should be discussed with project maintainers
- README must be updated to reflect any framework changes"# Society" 
