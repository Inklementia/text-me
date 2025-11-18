# Text me 💬  
Text-based dating sim with local LLM characters

Small experimental project: a text-based dating simulator where **each love interest is powered by a different local LLM** running through [Ollama](https://ollama.com/).  

Instead of one model pretending to be everyone, each character has their own model, personality prompt, and vibe.

### ✨ Features

- Six different datable characters, each backed by a different LLM
- Local-only inference via Ollama (no external API keys)
- Text-message-style UI for chatting with characters
- Personality prompts tuned for dating-sim style small talk & flirting

### 🧩 Requirements

- **Ollama** installed  
  Download & install from: https://ollama.com/

- **Models used in this project**

  | # | Character | Model name        | Description                  |
  |---|-----------|-------------------|------------------------------|
  | 1 | Lliam     | `llama3.2:1b`     | 22 y/o shy researcher        |
  | 2 | Diper     | `deepseek-r1:1.5b`| 27 y/o cold, overthinking guy|
  | 3 | Gemman    | `gemma3:1b`       | 24 y/o soft librarian        |
  | 4 | Phill     | `phi3:latest`     | 19 y/o genius prodigy        |
  | 5 | Qwen      | `qwen2.5:1.5b`    | 18 y/o chaotic robotics kid  |
  | 6 | Mistral   | `mistral:latest`  | 25 y/o cocky speedrunner     |

#### Pull them via Ollama (examples):
```bash
ollama pull llama3.2:1b
ollama pull deepseek-r1:1.5b
ollama pull gemma3:1b
ollama pull phi3:latest
ollama pull qwen2.5:1.5b
ollama pull mistral:latest
