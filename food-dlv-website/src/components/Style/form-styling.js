import styled from "styled-components";

export const Input = styled.input`
  display: block;
  width: 100%;
  padding: 0.5rem 1rem;
  margin-top: 0.5rem;
  --tw-text-opacity: 1;
  color: rgb(64, 64, 64);
  background-color: white;
  border-width: 1px;
  border-radius: 0.375rem;
  transition: 0.5s;
  &:focus {
    box-shadow: 0 0 5pt 2pt #d3d3d3;
    outline-width: 0px;
  }
`;

export const Label = styled.label`
  display: block;
  font-size: 1rem;
  line-height: 1.5rem;
  font-weight: 600;
  color: rgb(31 41 55 / var(--tw-text-opacity));
`;

export const Button = styled.button`
  width: 100%;
  padding: 0.5rem 1rem;
  letter-spacing: 0.025em;
  color: #fff;
  transition-property: color, background-color, border-color,
    text-decoration-color, fill, stroke;
  transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
  transition-duration: 200ms;
  transform: translate(var(--tw-translate-x), var(--tw-translate-y))
    rotate(var(--tw-rotate)) skewX(var(--tw-skew-x)) skewY(var(--tw-skew-y))
    scaleX(var(--tw-scale-x)) scaleY(var(--tw-scale-y));
  background-color: #000;
  border-radius: 0.375rem;
  &:hover {
    background-color: rgb(82, 82, 91);
  }
  &:focus {
    outline: 2px solid transparent;
    outline-offset: 2px;
    background-color: rgb(82, 82, 91);
  }
`;

export const Box = styled.div`
  width: 100%;
  padding: 1.5rem;
  margin: auto;
  background-color: #fff;
  border-radius: 0.375rem;
  --tw-shadow: 0 10px 15px -3px rgb(0 0 0 / 0.1),
    0 4px 6px -4px rgb(0 0 0 / 0.1);
  --tw-shadow-colored: 0 10px 15px -3px var(--tw-shadow-color),
    0 4px 6px -4px var(--tw-shadow-color);
  --tw-ring-color: rgb(64, 64, 64);
  --tw-ring-offset-shadow: var(--tw-ring-inset) 0 0 0
    var(--tw-ring-offset-width) var(--tw-ring-offset-color);
  --tw-ring-shadow: var(--tw-ring-inset) 0 0 0
    calc(3px + var(--tw-ring-offset-width)) var(--tw-ring-color);
  box-shadow: var(--tw-ring-offset-shadow, 0 0 #0000),
    var(--tw-ring-shadow, 0 0 #0000), var(--tw-shadow);
  max-width: 28rem;
`;
