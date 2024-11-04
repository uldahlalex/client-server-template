module.exports = {
    env: {
        browser: true,
        es2021: true,
    },
    extends: [
        "eslint:recommended",
        "plugin:@typescript-eslint/recommended",
        "plugin:react/recommended",
        "plugin:react-hooks/recommended",
    ],
    overrides: [
        {
            env: {
                node: true,
            },
            files: [".eslintrc.{js,cjs}"],
            parserOptions: {
                sourceType: "script",
            },
        },
        {
            files: ["**/*.ts", "**/*.tsx"],
            rules: {
                "@typescript-eslint/explicit-module-boundary-types": "warn",
                "react/jsx-uses-react": "off",
                "react/jsx-uses-vars": "error",
                "@typescript-eslint/no-unused-vars": "warn",
                "react-hooks/rules-of-hooks": "error",
                "react-hooks/exhaustive-deps": "warn",
            },
        },
        {
            files: ["src/**/index.ts", "imports.ts"],
            rules: { "no-restricted-imports": "off" },
        },
    ],
    parser: "@typescript-eslint/parser",
    parserOptions: {
        ecmaVersion: "latest",
        sourceType: "module",
        ecmaFeatures: {
            jsx: true,
        },
        project: "./tsconfig.json",
    },
    plugins: [
        "sort-exports",
        "@typescript-eslint",
        "react",
        "react-hooks",
        "react-compiler",
        "import",
    ],
    rules: {
        "import/no-relative-parent-imports": "error",
        "sort-exports/sort-exports": [
            "error",
            { sortDir: "asc", ignoreCase: true, sortExportKindFirst: "type" },
        ],
        "import/order": [
            "error",
            {
                groups: [
                    "builtin",
                    "external",
                    "internal",
                    "parent",
                    "sibling",
                    "index",
                ],
                "newlines-between": "always",
                alphabetize: {
                    order: "asc",
                    caseInsensitive: true,
                },
                pathGroups: [
                    {
                        pattern: "./forms/**",
                        group: "internal",
                        position: "after",
                    },
                ],
                pathGroupsExcludedImportTypes: ["builtin"],
            },
        ],
        "no-restricted-imports": [
            "error",
            {
                patterns: [
                    {
                        "group": [
                            "../**/hooks/!(imports.ts)",
                            "hooks/!(imports.ts)",
                            "../**/helpers/!(imports.ts)",
                            "helpers/!(imports.ts)",
                            "../**/models/!(imports.ts)",
                            "models/!(imports.ts)",
                            "../**/atoms/!(imports.ts)",
                            "atoms/!(imports.ts)"
                        ],
                        message: "Import from index.ts instead",
                    },
                ],
            },
        ],
        "react-compiler/react-compiler": "error",
        "@typescript-eslint/no-unused-vars": "off",
        "react/react-in-jsx-scope": "off",
        "@typescript-eslint/no-explicit-any": "off",
        "react/prop-types": "off",
        "no-console": "warn",
        "react/jsx-key": ["error", { checkFragmentShorthand: true }],
        "@typescript-eslint/ban-ts-comment": "off",
    },
    settings: {
        react: {
            version: "detect",
        },
    },
};
