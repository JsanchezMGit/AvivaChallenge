import { defineConfig, loadEnv } from 'vite';
import react from '@vitejs/plugin-react'


export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), ['VITE_API_KEY']);
  
  return {
    define: {
      'import.meta.env.VITE_API_KEY': JSON.stringify(env.VITE_API_KEY)
    },
    plugins: [react()],
    envPrefix: 'VITE_',
  };
});