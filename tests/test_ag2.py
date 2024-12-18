# tests/test_ag2.py
from autogen import AssistantAgent, UserProxyAgent, config_list_from_json

def test_basic_agent():
    config_list = config_list_from_json("OAI_CONFIG_LIST")
    assistant = AssistantAgent("test_agent", llm_config={"config_list": config_list})
    user_proxy = UserProxyAgent("user_proxy")
    
    # Test basic interaction
    user_proxy.initiate_chat(assistant, message="What kind of agent are you?")