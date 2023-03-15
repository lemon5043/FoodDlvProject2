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
  color: white;
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
