module.exports = {
  env: {
    node: true,
    jest: true,
  },
  extends: ['import:recommended', 'airbnb-typescript/base'],
  parserOptions: {
    project: './tsconfig.json'
  },
  rules: {
    '@typescript-eslint/no-unused-vars': ['error', { argsIgnorePattern: '^_' }],
  }
}