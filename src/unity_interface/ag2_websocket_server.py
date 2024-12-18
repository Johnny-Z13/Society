import asyncio
import websockets
import logging
from typing import Set

class AG2UnityServer:
    """
    A WebSocket server that communicates with Unity.
    """
    
    def __init__(self, host: str = "localhost", port: int = 8765):
        self.host = host
        self.port = port
        self.connected_clients: Set[websockets.WebSocketServerProtocol] = set()
        self.setup_logging()

    def setup_logging(self):
        logging.basicConfig(
            level=logging.INFO,
            format='%(asctime)s - %(levelname)s - %(message)s'
        )
        self.logger = logging.getLogger(__name__)

    async def register_client(self, websocket: websockets.WebSocketServerProtocol):
        self.connected_clients.add(websocket)
        self.logger.info(f"New client connected. Total clients: {len(self.connected_clients)}")

    async def unregister_client(self, websocket: websockets.WebSocketServerProtocol):
        self.connected_clients.remove(websocket)
        self.logger.info(f"Client disconnected. Total clients: {len(self.connected_clients)}")

    async def handle_client(self, websocket):  # Removed 'path' parameter
        """Handle incoming messages from a client."""
        await self.register_client(websocket)
        try:
            async for message in websocket:
                self.logger.info(f"Received message: {message}")
                # Echo the message back for now
                await websocket.send(f"Server received: {message}")
        except websockets.ConnectionClosed as e:
            self.logger.warning(f"Connection closed: {e}")
        except Exception as e:
            self.logger.error(f"Error handling message: {e}")
        finally:
            await self.unregister_client(websocket)

    def run(self):
        """Run the WebSocket server."""
        self.logger.info(f"Starting server at ws://{self.host}:{self.port}")
        
        async def start_server():
            async with websockets.serve(self.handle_client, self.host, self.port):
                self.logger.info("Server started. Waiting for connections...")
                await asyncio.Future()  # Keep the server running forever

        try:
            asyncio.run(start_server())
        except KeyboardInterrupt:
            self.logger.info("Server shutting down...")
        except Exception as e:
            self.logger.error(f"Server error: {e}")

if __name__ == "__main__":
    server = AG2UnityServer()
    server.run()