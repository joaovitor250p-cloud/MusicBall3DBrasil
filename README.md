# Nuvem Entrega Rápida

Aplicativo web instalável (PWA) para organizar rotas. **Projeto independente, sem integração ou vínculo oficial com a Shopee/SPX.** Não pede dados de login ou senhas e não consulta ofertas oficiais.

## Publicar com GitHub Pages
1. Crie um repositório público novo chamado `nuvem-entrega-rapida`.
2. Envie para a raiz os arquivos `index.html`, `manifest.webmanifest`, `sw.js`, `icon-192.png` e `icon-512.png`.
3. Em **Settings > Pages**, escolha **Deploy from a branch**, branch `main`, pasta `/(root)` e salve.
4. Após ativar o Pages, a URL normalmente será `https://SEU-USUARIO.github.io/nuvem-entrega-rapida/`. Verifique no próprio GitHub a URL final.
5. No Chrome Android, abra a URL em HTTPS e toque no pequeno ícone de nuvem no cabeçalho para instalar. Se não aparecer o prompt automático, use o menu ⋮ > Instalar app / Adicionar à tela inicial.

O armazenamento usa localStorage por aparelho/navegador; rotas não sincronizam entre celulares. A instalação por PWA depende de hospedagem HTTPS, não funciona abrindo `index.html` como arquivo local.
