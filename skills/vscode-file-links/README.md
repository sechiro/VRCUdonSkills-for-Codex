# vscode-file-links

`vscode-file-links` is a Codex skill for formatting file references so they stay clickable and consistent in the VS Code Codex chat UI.

It enforces a simple rule set:

- Use standalone inline-code file references
- Use `@` plus an absolute path
- Use `/` separators on Windows
- Add `#Lline` or `:line[:column]` when needed
- Avoid `file://`, `vscode://`, webview URLs, and Markdown-link fallbacks

## Repository Layout

- `SKILL.md`: skill definition and workflow
- `agents/openai.yaml`: UI metadata and invocation policy
- `references/link-rules.md`: reusable prompt block for file-reference rules

## Install

Clone or copy this repository to a Codex skills directory so the folder name remains `vscode-file-links`.

Common location:

```text
~/.codex/skills/vscode-file-links
```

On Windows, that is typically:

```text
C:/Users/<your-user>/.codex/skills/vscode-file-links
```

## Use

Explicit invocation example:

```text
Use $vscode-file-links at C:/Users/<your-user>/.codex/skills/vscode-file-links and format file references for this session.
```

You can also adapt the reusable rules block in `references/link-rules.md` when you want to paste the formatting rules directly into a prompt.

## Validate

If you have the OpenAI skill tooling available, run:

```text
python <path-to-skill-creator>/scripts/quick_validate.py <path-to-vscode-file-links>
```

The skill is valid when the validator accepts:

- `SKILL.md` frontmatter
- the skill name
- the repository folder structure

## Notes

- This skill depends on the current file-reference behavior of the VS Code Codex chat UI.
- Future extension changes may affect which file-reference formats remain clickable.
- The default policy in this repository keeps implicit invocation disabled so users can opt in explicitly first.

## Author

- `sechiro`

## License

- MIT
