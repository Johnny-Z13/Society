import sys
import os

# Add the src directory to the path
sys.path.append(os.path.join(os.path.dirname(__file__), "..", "src"))

from unity_interface.ag2_websocket_server import AG2UnityServer

if __name__ == "__main__":
    server = AG2UnityServer()
    server.run()
