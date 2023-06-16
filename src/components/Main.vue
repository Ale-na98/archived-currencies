<template>
    <main class="container">
        <article class="card disclaimer-card">
            <header class="d-flex disclaimer-header">
                <span>DISCLAIMER</span>
                <div class="close-indicator" type="button" data-bs-toggle="collapse" data-bs-target="#collapseDisclaimer"
                    aria-expanded="true" aria-controls="collapseDisclaimer">
                    <svg xmlns="http://www.w3.org/2000/svg" width="14" height="10" role="presentation"
                        @click="showDisclaimer = !showDisclaimer"
                        :class="showDisclaimer ? 'showDisclaimer' : 'hideDisclaimer'">
                        <path
                            d="M9.211364 7.59931l4.48338-4.867229c.407008-.441854.407008-1.158247 0-1.60046l-.73712-.80023c-.407008-.441854-1.066904-.441854-1.474243 0L7 5.198617 2.51662.33139c-.407008-.441853-1.066904-.441853-1.474243 0l-.737121.80023c-.407008.441854-.407008 1.158248 0 1.600461l4.48338 4.867228L7 10l2.211364-2.40069z">
                        </path>
                    </svg>
                </div>
            </header>
            <div class="content-disclaimer collapse show" id="collapseDisclaimer">
                <p class="my-0">
                    Disclaimer body
                </p>
            </div>
        </article>
        <div class="container py-3">
            <div class="row">
                <form class="search-data" method="POST" name="post_from_date">
                    <div class="row align-items-end">
                        <div class="col-lg-3 py-2">
                            <label for="currency-select"> Choose the currency:</label>
                            <select class="form-select" id="currency-select" name="currency-select">
                                <option value="currency-1"> 1</option>
                                <option value="currency-2"> 2</option>
                            </select>
                        </div>
                        <div class="col-lg-3 py-2">
                            <label for="date-select"> Enter the transaction date:</label>
                            <input class="form-control" type="date" />
                        </div>
                        <div class="col-lg-1 py-2 text-center">
                            <button class="btn btn-secondary" type="submit">Search</button>
                        </div>
                    </div>
                </form>
                <article class="my-3">
                    <div class="row align-items-center">
                        <div class="col-lg-3 py-2">
                            The nearest date preceding the transaction:
                        </div>
                        <div class="col-lg-1 py-2 ">
                            <strong>12.05.2022</strong>
                        </div>
                    </div>
                    <div class="row align-items-center">
                        <div class="col-lg-3 py-2">
                            Currency exchange rate for 1 unit:
                        </div>
                        <div class="col-lg-3 py-2">
                            <strong>0,055 PLN</strong>
                        </div>
                    </div>
                    <div class="row align-items-center">
                        <div class="col-lg-3 py-2">
                            Official archive on the entered date:
                        </div>
                        <div class="col-lg-1 py-2">
                                <a
                                    href="/">Link</a>
                        </div>
                    </div>
                </article>
            </div>
        </div>
    </main>
</template>

<script>
import 'bootstrap/js/dist/collapse'

export default {
    data() {
        return {
            showDisclaimer: true
        }
    }
}
</script>

<style>
.disclaimer-card {
    padding: 0.75rem;
    margin-top: 2rem;
    margin-bottom: 2rem;
    color: #664d03;
    border-color: #ffecb5;
    background-color: #fff3cd;
}

.disclaimer-header {
    font-weight: bold;
    padding-top: 0.25rem;
    padding-bottom: 0.25rem;
    justify-content: space-between;
}

.close-indicator {
    fill: #664d03;
    transform: rotate(180deg);
}

.showDisclaimer {
    transform: rotate(0deg);
    transition-timing-function: cubic-bezier(1, 0.5, 0.8, 1);
    transition-duration: var(--vs-transition-duration);
}

.hideDisclaimer {
    transform: rotate(-180deg);
    transition-timing-function: cubic-bezier(1, 0.5, 0.8, 1);
    transition-duration: var(--vs-transition-duration);
}

.content-disclaimer {
    transition-timing-function: cubic-bezier(1, 0.5, 0.8, 1);
    transition-duration: var(--vs-transition-duration);
}

.content-disclaimer a {
    font-weight: bold;
    --bs-link-color: #664d03;
    --bs-link-hover-color: var(--bs-dark);
}
</style>
